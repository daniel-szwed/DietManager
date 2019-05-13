using DietManager.ViewModels;
using System.Windows;

namespace DietManager.Views
{
    /// <summary>
    /// Interaction logic for NutritionFacts.xaml
    /// </summary>
    public partial class NutritionFacts : Window
    {
        public NutritionFacts()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).Container.Resolve(typeof(INutritionFactsViewModel), "NutritionFactsViewModel");
        }
    }
}
