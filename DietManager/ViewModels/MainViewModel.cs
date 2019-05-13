using DietManager.Commands;
using DietManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietManager.ViewModels
{
    public class MainViewModel
    {
        public Command ManageIngredients { get; }
        public Command NutritionFacts { get; }

        public MainViewModel()
        {
            ManageIngredients = new Command(OnManageIngredients);
            NutritionFacts = new Command(OnNutritionFacts);
        }

        private void OnNutritionFacts(object obj)
        {
            var window = new NutritionFacts();
            window.Show();
        }

        private void OnManageIngredients(object p)
        {
            var window = new IngredientView();
            window.Show();
        }
    }
}
