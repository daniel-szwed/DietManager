using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public interface IUpdateNutritionFactViewModel
    {
        public ICommand Update { get; }
        public Models.NutritionFact SelectedItem { get; set; }
    }
}
