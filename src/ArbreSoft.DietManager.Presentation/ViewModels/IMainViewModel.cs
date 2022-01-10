using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public interface IMainViewModel
    {
        ICommand BrowseNutritionFacts { get; }
        ICommand AddMeal { get; }
    }
}
