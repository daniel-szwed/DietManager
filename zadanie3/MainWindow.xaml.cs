using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using zadanie3.Models;

namespace zadanie3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Movie> Movies { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Movies = new ObservableCollection<Movie>() {
                new Movie() {Name = "Droga do szczęścia", Gener = Gener.Drama, Year = 2008, AgeLimit = 12},
                new Movie() {Name = "Apartament", Gener = Gener.Drama, Year = 2004, AgeLimit = 12},
                new Movie() {Name = "Zielona mila", Gener = Gener.Drama, Year = 1999, AgeLimit = 12},
                new Movie() {Name = "Lot nad kukułczym gniazdem", Gener = Gener.Drama, Year = 1975, AgeLimit = 16},
                new Movie() {Name = "Świat dzikiego zachodu", Gener = Gener.Drama, Year = 1973, AgeLimit = 16}
            };
            MoviesListBox.ItemsSource = Movies;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int ageLimit;
            if(!Int32.TryParse(AgeLimit.Text, out ageLimit))
            {
                return;
            }
            var newMovie = new Movie()
            {
                Name = Title.Text,
                AgeLimit = ageLimit,
            };
            Movies.Add(newMovie);
        }
    }
}
