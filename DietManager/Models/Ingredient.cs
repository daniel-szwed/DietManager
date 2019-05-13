using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DietManager.Models
{
    public class Ingredient : ModelBase
    {
        private string _name;
        private float _kcal;
        private float _protein;
        private float _carbohydrates;
        private float _sugar;
        private float _fat;
        private float _saturated;

        [Key]
        public int Id { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(nameof(Name)); }
        }

        public float Kcal
        {
            get { return _kcal; }
            set { _kcal = value; RaisePropertyChanged(nameof(Kcal)); }
        }
        
        public float Protein
        {
            get { return _protein; }
            set { _protein = value; RaisePropertyChanged(nameof(Protein)); }
        }
        
        public float Carbohydrates
        {
            get { return _carbohydrates; }
            set { _carbohydrates = value; RaisePropertyChanged(nameof(Carbohydrates)); }
        }
        
        public float Sugar
        {
            get { return _sugar; }
            set { _sugar = value; RaisePropertyChanged(nameof(Sugar)); }
        }
        
        public float Fat
        {
            get { return _fat; }
            set { _fat = value; RaisePropertyChanged(nameof(Fat)); }
        }


        public float Saturated
        {
            get { return _saturated; }
            set { _saturated = value; RaisePropertyChanged(nameof(Saturated)); }
        }
    }
}
