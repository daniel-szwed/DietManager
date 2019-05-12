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

        public MainViewModel()
        {
            ManageIngredients = new Command(OnManageIngredients);
        }

        private void OnManageIngredients()
        {
            var window = new IngredientView();
            window.Show();
        }
    }
}
