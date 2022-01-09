using System.Windows.Controls;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public interface INutritionFactViewModel : IAddNutritionFactViewModel, IUpdateNutritionFactViewModel
    {
        void OnSearchQueryChanged(object sender, string searchQuery);
        ICommand Remove { get; }
        ICommand SearchOnline { get; }
    }
}
