using ArbreSoft.DietManager.Infrastructure;
using ArbreSoft.DietManager.Presentation.ViewModels;
using System.Windows;

namespace ArbreSoft.DietManager.Presentation.Views
{
    /// <summary>
    /// Interaction logic for IngredientView.xaml
    /// </summary>
    public partial class NutritionFactView : Window
    {
        public NutritionFactView()
        {
            InitializeComponent();
            DataContext = AppServiceProvider.Instance.GetService<INutritionFactViewModel>();
        }

        private void SearchQuery_TextChanged(object sender, string searchQuery)
        {
            (DataContext as INutritionFactViewModel)?.OnSearchQueryChanged(sender, searchQuery);
        }
    }
}