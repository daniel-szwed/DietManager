using DietManager.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DietManager.ViewModels
{
    public interface IIngredientViewModel
    {
        ICommand AddIngredient { get; }
        ICommand UpdateIngredient { get; }
        ICommand RemoveIngredient { get; }
        ICommand SearchIngredient { get; }
        ObservableCollection<IngredientBase> Ingredients { get; set; }
    }
}
