using DietManager.Commands;
using DietManager.Models;
using DietManager.Repositories;
using DietManager.Views;
using System;
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
        private Meal _selectedMeal;
        private Ingredient _selectedIngredient;

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
                try
                {
                    SelectedMeal = Meals.First(m => m.Ingregients.Contains(_selectedIngredient));
                }
                catch (InvalidOperationException) { }
                NotifyPropertyChanged(nameof(SelectedIngredient)); }
        }
        
        internal void IncreaseAmount()
        {
            if (SelectedMeal?.Ingregients.Contains(SelectedIngredient) ?? false)
                SelectedIngredient.Amount++;
        }

        internal void DecreaseAmount()
        {
            if (SelectedMeal?.Ingregients.Contains(SelectedIngredient) ?? false)
                SelectedIngredient.Amount--;
        }

        public ICommand ManageIngredients
        {
            get { return new EagerCommand((parameters) => OnManageIngredients(parameters)); }
        }

        public ICommand AddMeal
        {
            get { return new EagerCommand(p => OnAddMeal(p), p => CanAddMeal); }
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

        public MainViewModel(IMealRepository mealRepository, IIngredientBaseRepository ingredientBaseRepository, IIngredientRepository ingredientRepository)
        {
            _ingredientBaseRepository = ingredientBaseRepository;
            _ingredientRepository = ingredientRepository;
            _mealRepository = mealRepository;
            Meals = new ObservableCollection<Meal>(mealRepository.GetAllAsync().GetAwaiter().GetResult());
            IngredientBase = new ObservableCollection<IngredientBase>(GetIngredients());
        }

        private IEnumerable<IngredientBase> GetIngredients()
        {
            return _ingredientBaseRepository.GetAllAsync().GetAwaiter().GetResult();
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
            _ingredientRepository.RemoveAsync(ingredient);
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
            _mealRepository.UpdateAsync(meal);
        }

        public bool CanAddMeal { get { return true; } }

        private async void OnAddMeal(object p)
        {
            var meal = new Meal() { Name = p.ToString() };
            var result = await _mealRepository.AddAsync(meal);
            if (result == 1)
                Meals.Add(meal);
        }

        private void OnManageIngredients(object parameters)
        {
            var window = new IngredientView();
            window.Show();
        }
    }
}
