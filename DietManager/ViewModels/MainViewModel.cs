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
        private IIngredientRepository _ingredientRepository;
        private IMealRepository _mealRepository;
        public ObservableCollection<Meal> Meals { get; set; }
        public ObservableCollection<IngredientBase> Ingredients { get; set; }

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


        public MainViewModel(IMealRepository mealRepository, IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
            _mealRepository = mealRepository;
            Meals = new ObservableCollection<Meal>(mealRepository.GetAllAsync().GetAwaiter().GetResult());
            Ingredients = new ObservableCollection<IngredientBase>(GetIngredients());
        }

        private IEnumerable<IngredientBase> GetIngredients()
        {
            return _ingredientRepository.GetAllAsync().GetAwaiter().GetResult();
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
        }

        private void OnRefreshInfredients(object p)
        {
            var ingredients = GetIngredients();
            Ingredients.Clear();
            ingredients.ToList().ForEach(i => Ingredients.Add(i));
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
