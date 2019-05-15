using DietManager.Commands;
using DietManager.Models;
using DietManager.Repositories;
using DietManager.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DietManager.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        private IIngredientRepository _ingredientRepository;
        private IMealRepository _mealRepository;

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
            get { return new EagerCommand(p => OnAddIngredient(p), p => CanAddIngredient); }
        }


        public MainViewModel(IMealRepository mealRepository, IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
            _mealRepository = mealRepository;
            Meals = new ObservableCollection<Meal>(mealRepository.GetAllAsync().GetAwaiter().GetResult());
            Ingredients = new ObservableCollection<Ingredient>(ingredientRepository.GetAllAsync().GetAwaiter().GetResult());
        }

        public ObservableCollection<Meal> Meals { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        
        public bool CanAddIngredient { get { return true; } }

        private void OnAddIngredient(object p)
        {
            var param = p as object[];
            var ingredient = param[0] as Ingredient;
            var meal = param[1] as Meal;
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
