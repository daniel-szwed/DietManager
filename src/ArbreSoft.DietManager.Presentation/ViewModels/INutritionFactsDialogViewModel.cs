using ArbreSoft.DietManager.Presentation.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public interface INutritionFactsDialogViewModel
    {
        ObservableCollection<NutritionFact> Items { get; set; }
        public Models.NutritionFact NutritionFact { get; set; }
        public string ButtonText { get; }
        public string ButtonIcon { get; }
        public ICommand Submit { get; }
        public ICommand Add { get; }
        public ICommand Update { get; }
    }
}
