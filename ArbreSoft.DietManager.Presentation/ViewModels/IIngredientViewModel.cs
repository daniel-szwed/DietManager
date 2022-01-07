using ArbreSoft.DietManager.Presentation.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public interface IIngredientViewModel
    {
        ICommand AddIngredient { get; }
        ICommand UpdateIngredient { get; }
        ICommand RemoveIngredient { get; }
        ICommand SearchIngredient { get; }
        ObservableCollection<NutritionFact> Ingredients { get; set; }
    }
}
