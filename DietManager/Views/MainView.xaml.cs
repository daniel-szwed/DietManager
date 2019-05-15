using DietManager.ViewModels;
using System.Windows;

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
    }
}
