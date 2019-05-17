using System.ComponentModel.DataAnnotations;

namespace DietManager.Models
{
    public class IngredientBase : BindableBase
    {
        protected string _name;
        protected float _kcal;
        protected float _protein;
        protected float _carbohydrates;
        protected float _sugar;
        protected float _fat;
        protected float _saturated;
        
        [Key]
        public int Id { get; set; }

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
    }
}
