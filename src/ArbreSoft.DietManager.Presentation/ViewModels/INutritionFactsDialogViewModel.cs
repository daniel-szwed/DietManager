using ArbreSoft.DietManager.Presentation.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public interface INutritionFactsDialogViewModel
    {
        public Models.NutritionFact NutritionFact {get; set;}
        public string ButtonText { get; }
        public ICommand Submit { get; }
        public ICommand Add { get; }
        public ICommand Update { get; }
        ObservableCollection<NutritionFact> Items { get; set; }
    }
}
