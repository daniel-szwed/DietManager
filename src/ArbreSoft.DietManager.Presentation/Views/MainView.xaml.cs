using ArbreSoft.DietManager.Infrastructure;
using ArbreSoft.DietManager.Presentation.Models;
using ArbreSoft.DietManager.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = AppServiceProvider.Instance.GetService<IMainViewModel>();
        }

        private void IngrAmount_TextChanged(object sender, System.EventArgs e)
        {
            ((TextBox)sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
            ((TextBox)sender).Focus();
        }

        private void Ammount_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            var ingr = (Ingredient)((DockPanel)sender).DataContext;
            if (e.Delta > 0)
                vm.IncreaseAmountAsync(ingr);
            else
                vm.DecreaseAmountAsync(ingr);
        }

        private void IngredientsListBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ListBox)sender).SelectedItem = null;
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
