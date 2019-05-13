using DietManager.Commands;

namespace DietManager.ViewModels
{
    public interface INutritionFactsViewModel
    {
        Command GetNutritionFacts { get; }
    }
}
