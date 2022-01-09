using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public interface IMainViewModel
    {
        ICommand ManageIngredients { get; }
        ICommand AddMeal { get; }
    }
}
