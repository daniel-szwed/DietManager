using DietManager.Models;
using DietManager.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DietManager.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).Container.Resolve(typeof(IMainViewModel), "MainViewModel");
        }

        private void IngrAmount_TextChanged(object sender, System.EventArgs e)
        {
            ((TextBox)sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
            ((TextBox)sender).Focus();
        }

        private void Ammount_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var vm = ((MainViewModel)DataContext);
            var ingr = (Ingredient)((DockPanel)sender).DataContext;
            if (e.Delta > 0)
                vm.IncreaseAmount(ingr);
            else
                vm.DecreaseAmount(ingr);
        }

        private void IngredientsListBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ListBox)sender).SelectedItem = null;
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
