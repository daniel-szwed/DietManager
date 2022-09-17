using ArbreSoft.DietManager.Infrastructure;
using ArbreSoft.DietManager.Presentation.ViewModels;
using ArbreSoft.Utils.ObjectExtensions;
using System.Collections.ObjectModel;
using System.Windows;

namespace ArbreSoft.DietManager.Presentation.Views
{
    /// <summary>
    /// Interaction logic for AddIngredient.xaml
    /// </summary>
    public partial class NutritionFactDialogView : Window
    {
        public NutritionFactDialogView(ObservableCollection<Models.NutritionFact> items, Models.NutritionFact selectedItem = null)
        {
            InitializeComponent();
            DataContext = AppServiceProvider.Instance.GetService<INutritionFactsDialogViewModel>();
            (DataContext as INutritionFactsDialogViewModel).Items = items;
            (DataContext as INutritionFactsDialogViewModel).NutritionFact = selectedItem.Clone();
        }
    }
}
