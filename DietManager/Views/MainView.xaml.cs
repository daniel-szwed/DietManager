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
            if (e.Delta > 0)
                vm.IncreaseAmount();
            else
                vm.DecreaseAmount();
        }

        private void IngredientsListBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ListBox)sender).SelectedItem = null;
        }
    }
}
