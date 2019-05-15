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

        public MainViewModel(IMealRepository mealRepository, IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
            _mealRepository = mealRepository;
            Meals = new ObservableCollection<Meal>(mealRepository.GetAllAsync().GetAwaiter().GetResult());
        }

        public ObservableCollection<Meal> Meals { get; set; }

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
