using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Infrastructure;
using ArbreSoft.DietManager.Presentation.Commands;
using ArbreSoft.DietManager.Presentation.Models;
using ArbreSoft.DietManager.Presentation.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.Controls.Custom
{
    /// <summary>
    /// Interaction logic for MeaslList.xaml
    /// </summary>
    public partial class MealsList : UserControl
    {
        private readonly IMediator _mediator;

        public MealsList()
        {
            InitializeComponent();

            MouseWheel += Ammount_MouseWheel;
            LostFocus += IngredientsListBox_LostFocus;

            _mediator = AppServiceProvider.Instance.GetService<IMediator>();
        }

        public static DependencyProperty MealsProperty = DependencyProperty.Register("Meals", typeof(IList<Meal>), typeof(MealsList), new FrameworkPropertyMetadata(new List<Meal>()));
        public static DependencyProperty MealProperty = DependencyProperty.Register("Meal", typeof(Meal), typeof(MealsList), new FrameworkPropertyMetadata(default(Meal)));
        public static DependencyProperty SelectedMealProperty = DependencyProperty.Register("SelectedMeal", typeof(Meal), typeof(MealsList), new FrameworkPropertyMetadata(default(Meal)));

        public IList<Meal> Meals
        {
            get => (ObservableCollection<Meal>)GetValue(MealsProperty);
            set => SetValue(MealsProperty, value);
        }

        public Meal SelectedMeal
        {
            get => (Meal)GetValue(SelectedMealProperty);
            set => SetValue(SelectedMealProperty, value);
        }

        public Meal Meal
        {
            get => (Meal)GetValue(MealProperty);
            set => SetValue(MealProperty, value);
        }

        public IMediator Mediator => _mediator;

        private void OnRemoveMealClick(object sender, RoutedEventArgs args)
        {
            var mealToRemove = Meals.First(meal => meal.Id == Meal.Id);
            Mediator.Send(new RemoveMealCommand(mealToRemove.Id));
            Meals.Remove(mealToRemove);
        }

        private Ingredient SelectedIngredient { get; set; }

        public void IngredientsListBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //((ListBox)sender).SelectedItem = null;
        }

        private void Ammount_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //var vm = (MainViewModel)DataContext;
            //var ingr = (Ingredient)((DockPanel)sender).DataContext;
            //if (e.Delta > 0)
            //    vm.IncreaseAmountAsync(ingr);
            //else
            //    vm.DecreaseAmountAsync(ingr);
        }
    }
}
