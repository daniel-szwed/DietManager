using ArbreSoft.DietManager.Presentation.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public interface IAddNutritionFactViewModel
    {
        public ICommand Add { get; }
        ObservableCollection<NutritionFact> Items { get; set; }
    }
}
