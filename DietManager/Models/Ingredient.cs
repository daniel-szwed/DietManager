using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DietManager.Models
{
    public class Ingredient : BindableBase
    {
        private string _name;
        private float _kcal;
        private float _protein;
        private float _carbohydrates;
        private float _sugar;
        private float _fat;
        private float _saturated;
        private int _amount;

        public Ingredient()
        {
            Meals = new HashSet<Meal>();
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

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; NotifyPropertyChanged(nameof(Amount)); }
        }

        public virtual ICollection<Meal> Meals { get; set; }
    }
}
