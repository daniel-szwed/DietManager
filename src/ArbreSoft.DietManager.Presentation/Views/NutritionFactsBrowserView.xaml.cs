using ArbreSoft.DietManager.Infrastructure;
using ArbreSoft.DietManager.Presentation.ViewModels;
using System.Windows;

namespace ArbreSoft.DietManager.Presentation.Views
{
    /// <summary>
    /// Interaction logic for IngredientView.xaml
    /// </summary>
    public partial class NutritionFactsBrowserView : Window
    {
        public NutritionFactsBrowserView()
        {
            InitializeComponent();
            DataContext = AppServiceProvider.Instance.GetService<INutritionFactsBrowserViewModel>();
        }

        private void SearchQuery_TextChanged(object sender, string searchQuery)
        {
            (DataContext as INutritionFactsBrowserViewModel)?.OnSearchQueryChanged(sender, searchQuery);
        }
    }
}