﻿using DietManager.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            //var bitmap = new BitmapImage(new Uri("../Media/search.png", UriKind.Relative));
            //SearchButton.Content = new Image
            //{
            //    Height = 20,
            //    Width = 20,
            //    Source = bitmap,
            //    VerticalAlignment = VerticalAlignment.Center
            //};
        }

        private void IngredientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((IIngredientViewModel)DataContext).UpdateIngredient.RaiseCanExecuteChanged();
            ((IIngredientViewModel)DataContext).RemoveIngredient.RaiseCanExecuteChanged();
        }
    }
}
