using DietManager.Commands;
using DietManager.Models;
using Data.Repositories;
using DietManager.Services;
using DietManager.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System;

namespace DietManager.ViewModels
{
    public class MainViewModel : BindableBase, IMainViewModel
    {
        private readonly IServiceProvider provider;
        private IIngredientBaseRepository ingredientBaseRepository;
        private IIngredientRepository ingredientRepository;
        private IMealRepository mealRepository;
        private IMealService mealService;
        private IImportExportService importExportService;
        private Meal selectedMeal;
        private Ingredient selectedIngredient;
        private IngredientBase totalNutritionFacts;

        public MainViewModel(IServiceProvider provider,
            IMealRepository mealRepository,
            IIngredientBaseRepository ingredientBaseRepository, 
            IIngredientRepository ingredientRepository, 
            IMealService mealService, 
            IImportExportService importExportService)
        {
            this.provider = provider;
            this.ingredientBaseRepository = ingredientBaseRepository;
            this.ingredientRepository = ingredientRepository;
            this.mealRepository = mealRepository;
            this.mealService = mealService;
            this.importExportService = importExportService;
            Meals = new ObservableCollection<Meal>(mealRepository.Find(x => true).GetAwaiter().GetResult());
            IngredientBase = new ObservableCollection<IngredientBase>(GetIngredients());
            CalcTotalNutritionFact();
        }

        public ObservableCollection<Meal> Meals { get; set; }
        public ObservableCollection<IngredientBase> IngredientBase { get; set; }
        
        public Meal SelectedMeal
        {
            get { return selectedMeal; }
            set { selectedMeal = value; NotifyPropertyChanged(nameof(SelectedMeal)); }
        }

        public Ingredient SelectedIngredient
        {
            get { return selectedIngredient; }
            set { selectedIngredient = value;
                if(selectedIngredient != null)
                    SelectedMeal = Meals.First(m => m.Ingregients.Contains(selectedIngredient));
                NotifyPropertyChanged(nameof(SelectedIngredient)); }
        }

        public IngredientBase TotalNutritionFacts
        {
            get { return totalNutritionFacts; }
            set { totalNutritionFacts = value; NotifyPropertyChanged(nameof(TotalNutritionFacts)); }
        }

        #region Commands
        public ICommand SaveToDataBase
        {
            get { return new EagerCommand(p => OnSaveToDataBase(p)); }
        }

        public ICommand ImportDiet
        {
            get { return new EagerCommand(p => OnImportDietAsync(p)); }
        }

        public ICommand ExportDiet
        {
            get { return new EagerCommand(p => OnExportDiet(p)); }
        }

        public ICommand ManageIngredients
        {
            get { return new EagerCommand((parameters) => OnManageIngredients(parameters)); }
        }

        public ICommand AddMeal
        {
            get { return new EagerCommand(p => OnAddMeal(p), p => CanAddMeal); }
        }

        public ICommand RemoveMeal
        {
            get { return new EagerCommand(p => OnRemoveMeal(p)); }
        }

        public ICommand AddIngredient
        {
            get { return new EagerCommand(p => OnAddIngredient(p), p => CanAddIngredient(p)); }
        }

        public ICommand RefreshIngredients
        {
            get { return new EagerCommand(p => OnRefreshInfredients(p)); }
        }

        public ICommand RemoveIngredient
        {
            get { return new EagerCommand(p => OnRemoveIngredient(p), p => CanRemoveIngredient(p)); }
        }
        #endregion

        #region Commands Implementation
        private void OnSaveToDataBase(object p)
        {
            mealRepository.UpdateRange(Meals);
            mealRepository.SaveChangesAsync();
        }

        private async void OnImportDietAsync(object p)
        {
            var meals = await importExportService.ImportAsync<Meal>();
            Meals.ToList().ForEach(m => mealRepository.Remove(m));
            Meals.Clear();
            meals.ToList().ForEach(m => {
                m.Ingregients.ToList().ForEach(i => ingredientRepository.Add(i));
                mealRepository.Add(m);
                Meals.Add(m);
            });
            mealRepository.SaveChangesAsync();
            CalcTotalNutritionFact();
        }

        private void OnExportDiet(object p)
        {
            importExportService.ExportAsync(Meals);
        }

        private void OnManageIngredients(object parameters)
        {
            var window = new IngredientView(provider);
            window.Show();
        }
       
        public bool CanAddMeal { get { return true; } }
        
        private async void OnAddMeal(object p)
        {
            var meal = new Meal() { Name = p.ToString() };
            mealRepository.Add(meal);
            var result = await mealRepository.SaveChangesAsync();
            if (result == 1)
                Meals.Add(meal);
        }

        private void OnRemoveMeal(object p)
        {
            Meal meal = p as Meal;
            Meals.Remove(meal);
            mealRepository.Remove(meal);
            mealRepository.SaveChangesAsync();
            CalcTotalNutritionFact();
        }

        private void OnRefreshInfredients(object p)
        {
            var ingredients = GetIngredients();
            IngredientBase.Clear();
            ingredients.ToList().ForEach(i => IngredientBase.Add(i));
        }
        
        private bool CanAddIngredient(object p)
        {
            var param = p as object[];
            var meal = param[0] as Meal;
            var ingredient = param[1] as IngredientBase;
            var ingredientAmount = param[2] as string;
            float ingrAmount;
            return meal != null && ingredient != null && float.TryParse(ingredientAmount, out ingrAmount);
        }

        private void OnAddIngredient(object p)
        {
            var param = p as object[];
            var meal = param[0] as Meal;
            var ingredient = new Ingredient(param[1] as IngredientBase);
            ingredient.Amount = float.Parse(param[2] as string);
            meal.Ingregients.Add(ingredient);
            mealRepository.Update(meal);
            mealRepository.SaveChangesAsync();
            CalcTotalNutritionFact();
        }

        private bool CanRemoveIngredient(object p)
        {
            var param = p as object[];
            var meal = param[0] as Meal;
            var ingredient = param[1] as Ingredient;
            return meal?.Ingregients.Contains(ingredient) ?? false;
        }

        private void OnRemoveIngredient(object p)
        {
            var param = p as object[];
            var meal = param[0] as Meal;
            var ingredient = param[1] as Ingredient;
            meal.Ingregients.Remove(ingredient);
            ingredientRepository.Remove(ingredient);
            ingredientRepository.SaveChangesAsync();
            CalcTotalNutritionFact();
        }
        #endregion

        private IEnumerable<IngredientBase> GetIngredients()
        {
            return ingredientBaseRepository.Find(x => true).GetAwaiter().GetResult();
        }

        private void CalcTotalNutritionFact()
        {
            TotalNutritionFacts = mealService.GetSum(Meals);
        }

        internal void IncreaseAmount(Ingredient ingredient)
        {
            ingredient.Amount++;
            CalcTotalNutritionFact();
        }

        internal void DecreaseAmount(Ingredient ingredient)
        {
            ingredient.Amount--;
            CalcTotalNutritionFact();
        }
    }
}