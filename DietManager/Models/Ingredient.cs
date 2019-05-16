using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DietManager.Models
{
    public class Ingredient : BindableBase
    {
        protected string _name;
        protected float _kcal;
        protected float _protein;
        protected float _carbohydrates;
        protected float _sugar;
        protected float _fat;
        protected float _saturated;
        private float _amount;

        public Ingredient()
        {
            Meals = new ObservableCollection<Meal>();
        }

        public Ingredient(IngredientBase ingredientBase)
        {
            Meals = new HashSet<Meal>();
            Name = ingredientBase.Name;
            Protein = ingredientBase.Protein;
            Carbohydrates = ingredientBase.Carbohydrates;
            Sugar = ingredientBase.Sugar;
            Fat = ingredientBase.Fat;
            Saturated = ingredientBase.Saturated;
        }

        [Key]
        public int IngredientId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public float Kcal
        {
            get { return _kcal; }
            set { _kcal = value; NotifyPropertyChanged(nameof(Kcal)); }
        }

        public float Protein
        {
            get { return _protein; }
            set { _protein = value; NotifyPropertyChanged(nameof(Protein)); }
        }

        public float Carbohydrates
        {
            get { return _carbohydrates; }
            set { _carbohydrates = value; NotifyPropertyChanged(nameof(Carbohydrates)); }
        }

        public float Sugar
        {
            get { return _sugar; }
            set { _sugar = value; NotifyPropertyChanged(nameof(Sugar)); }
        }

        public float Fat
        {
            get { return _fat; }
            set { _fat = value; NotifyPropertyChanged(nameof(Fat)); }
        }

        public float Saturated
        {
            get { return _saturated; }
            set { _saturated = value; NotifyPropertyChanged(nameof(Saturated)); }
        }

        public float Amount
        {
            get { return _amount; }
            set { _amount = value; NotifyPropertyChanged(nameof(Amount)); }
        }

        public virtual ICollection<Meal> Meals { get; set; }
    }
}