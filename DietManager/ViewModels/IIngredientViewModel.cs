using DietManager.Commands;
using DietManager.Models;
using System.Collections.ObjectModel;

namespace DietManager.ViewModels
{
    public interface IIngredientViewModel
    {
        Command AddIngredient { get; }
        ObservableCollection<Ingredient> Ingredients { get; set; }
    }
}
