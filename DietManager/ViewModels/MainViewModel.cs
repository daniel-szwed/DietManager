using DietManager.Commands;
using DietManager.Models;
using Data.Repositories;
using DietManager.Services;
using DietManager.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DietManager.ViewModels
{
    public class MainViewModel : BindableBase, IMainViewModel
    {
        private IIngredientBaseRepository _ingredientBaseRepository;
        private IIngredientRepository _ingredientRepository;
        private IMealRepository _mealRepository;
        private IMealService _mealService;
        private IImportExportService _importExportService;
        private Meal _selectedMeal;
        private Ingredient _selectedIngredient;
        private IngredientBase _totalNutritionFacts;

        public MainViewModel(IMealRepository mealRepository, IIngredientBaseRepository ingredientBaseRepository, IIngredientRepository ingredientRepository, IMealService mealService, IImportExportService importExportService)
        {
            _ingredientBaseRepository = ingredientBaseRepository;
            _ingredientRepository = ingredientRepository;
            _mealRepository = mealRepository;
            _mealService = mealService;
            _importExportService = importExportService;
            Meals = new ObservableCollection<Meal>(mealRepository.Find(x => true).GetAwaiter().GetResult());
            IngredientBase = new ObservableCollection<IngredientBase>(GetIngredients());
            CalcTotalNutritionFact();
        }

        public ObservableCollection<Meal> Meals { get; set; }
        public ObservableCollection<IngredientBase> IngredientBase { get; set; }
        
        public Meal SelectedMeal
        {
            get { return _selectedMeal; }
            set { _selectedMeal = value; NotifyPropertyChanged(nameof(SelectedMeal)); }
        }

        public Ingredient SelectedIngredient
        {
            get { return _selectedIngredient; }
            set { _selectedIngredient = value;
                if(_selectedIngredient != null)
                    SelectedMeal = Meals.First(m => m.Ingregients.Contains(_selectedIngredient));
                NotifyPropertyChanged(nameof(SelectedIngredient)); }
        }

        public IngredientBase TotalNutritionFacts
        {
            get { return _totalNutritionFacts; }
            set { _totalNutritionFacts = value; NotifyPropertyChanged(nameof(TotalNutritionFacts)); }
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
            _mealRepository.UpdateRange(Meals);
            _mealRepository.SaveChangesAsync();
        }

        private async void OnImportDietAsync(object p)
        {
            var meals = await _importExportService.ImportAsync<Meal>();
            Meals.ToList().ForEach(m => _mealRepository.Remove(m));
            Meals.Clear();
            meals.ToList().ForEach(m => {
                m.Ingregients.ToList().ForEach(i => _ingredientRepository.Add(i));
                _mealRepository.Add(m);
                Meals.Add(m);
            });
            _mealRepository.SaveChangesAsync();
            CalcTotalNutritionFact();
        }

        private void OnExportDiet(object p)
        {
            _importExportService.ExportAsync(Meals);
        }

        private void OnManageIngredients(object parameters)
        {
            var window = new IngredientView();
            window.Show();
        }

        public bool CanAddMeal { get { return true; } }
        
        private async void OnAddMeal(object p)
        {
            var meal = new Meal() { Name = p.ToString() };
            _mealRepository.Add(meal);
            var result = await _mealRepository.SaveChangesAsync();
            if (result == 1)
                Meals.Add(meal);
        }

        private void OnRemoveMeal(object p)
        {
            Meal meal = p as Meal;
            Meals.Remove(meal);
            _mealRepository.Remove(meal);
            _mealRepository.SaveChangesAsync();
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
            _mealRepository.Update(meal);
            _mealRepository.SaveChangesAsync();
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
            _ingredientRepository.Remove(ingredient);
            _ingredientRepository.SaveChangesAsync();
            CalcTotalNutritionFact();
        }
        #endregion

        private IEnumerable<IngredientBase> GetIngredients()
        {
            return _ingredientBaseRepository.Find(x => true).GetAwaiter().GetResult();
        }

        private void CalcTotalNutritionFact()
        {
            TotalNutritionFacts = _mealService.GetSum(Meals);
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