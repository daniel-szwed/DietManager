﻿using DietManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace DietManager.Views
{
    /// <summary>
    /// Interaction logic for IngredientView.xaml
    /// </summary>
    public partial class IngredientView : Window
    {
        public IngredientView(IServiceProvider provider)
        {
            InitializeComponent();
            DataContext = provider.GetService<IIngredientViewModel>();
        }
    }
}