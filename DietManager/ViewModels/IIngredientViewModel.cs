using DietManager.Commands;
using DietManager.Models;
using System.Collections.ObjectModel;

namespace DietManager.ViewModels
{
    public interface IIngredientViewModel
    {
        Command AddIngredient { get; }
        Command UpdateIngredient { get; }
        ObservableCollection<Ingredient> Ingredients { get; set; }
    }
}
