using ArbreSoft.DietManager.Infrastructure;
using ArbreSoft.DietManager.Presentation.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace ArbreSoft.DietManager.Presentation.Views
{
    /// <summary>
    /// Interaction logic for AddIngredient.xaml
    /// </summary>
    public partial class AddNutritionFactView : Window
    {
        public AddNutritionFactView(ObservableCollection<Models.NutritionFact> items, Models.NutritionFact selectedItem = null)
        {
            InitializeComponent();
            DataContext = AppServiceProvider.Instance.GetService<IAddNutritionFactViewModel>();
            (DataContext as IAddNutritionFactViewModel).Items = items;
            (DataContext as IUpdateNutritionFactViewModel).SelectedItem = selectedItem;
        }
    }
}
