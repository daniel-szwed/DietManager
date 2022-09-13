using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public interface INutritionFactsBrowserViewModel
    {
        void OnSearchQueryChanged(object sender, string searchQuery);
        ICommand Remove { get; }
        ICommand SearchOnline { get; }
    }
}
