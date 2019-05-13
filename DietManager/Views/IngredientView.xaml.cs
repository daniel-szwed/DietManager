using DietManager.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

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
            DataContext = ((App)Application.Current).Container.Resolve(typeof(IIngredientViewModel), "test");
        }

        private void IngredientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((IIngredientViewModel)DataContext).UpdateIngredient.RaiseCanExecuteChanged();
        }
    }
}
