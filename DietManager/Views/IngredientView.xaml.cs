using DietManager.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DietManager.Views
{
    /// <summary>
    /// Interaction logic for IngredientView.xaml
    /// </summary>
    public partial class IngredientView : Window
    {
        public IngredientView()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).Container.Resolve(typeof(IIngredientViewModel), "IngredientViewModel");
        }
    }
}