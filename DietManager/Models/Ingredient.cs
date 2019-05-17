﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DietManager.Models
{
    public class Ingredient : IngredientBase
    {
        private float _amount;

        public Ingredient()
        {
            Meals = new ObservableCollection<Meal>();
        }

        public Ingredient(IngredientBase ingredientBase)
        {
            Meals = new ObservableCollection<Meal>();
            Kcal = ingredientBase.Kcal;
            Name = ingredientBase.Name;
            Protein = ingredientBase.Protein;
            Carbohydrates = ingredientBase.Carbohydrates;
            Sugar = ingredientBase.Sugar;
            Fat = ingredientBase.Fat;
            Saturated = ingredientBase.Saturated;
        }

        public float Amount
        {
            get { return _amount; }
            set { _amount = value; NotifyPropertyChanged(nameof(Amount)); }
        }

        public virtual ICollection<Meal> Meals { get; set; }
    }
}