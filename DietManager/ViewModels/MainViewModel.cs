using DietManager.Commands;
using DietManager.Views;
using System.Windows.Input;

namespace DietManager.ViewModels
{
    public class MainViewModel
    {
        public ICommand ManageIngredients
        {
            get { return new EagerCommand((parameters) => OnManageIngredients(parameters)); }
        }

        private void OnManageIngredients(object parameters)
        {
            var window = new IngredientView();
            window.Show();
        }
    }
}
