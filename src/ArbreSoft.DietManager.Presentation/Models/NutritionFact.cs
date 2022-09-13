using System;

namespace ArbreSoft.DietManager.Presentation.Models
{
    public class NutritionFact : BindableBase
    {
        protected string _name;
        protected string _kcal;
        protected string _protein;
        protected string _carbohydrates;
        protected string _sugar;
        protected string _fat;
        protected string _saturated;

        public Guid Id { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public string Kcal
        {
            get { return _kcal; }
            set { _kcal = value; NotifyPropertyChanged(nameof(Kcal)); }
        }

        public string Protein
        {
            get { return _protein; }
            set { _protein = value; NotifyPropertyChanged(nameof(Protein)); }
        }

        public string Carbohydrates
        {
            get { return _carbohydrates; }
            set { _carbohydrates = value; NotifyPropertyChanged(nameof(Carbohydrates)); }
        }

        public string Sugar
        {
            get { return _sugar; }
            set { _sugar = value; NotifyPropertyChanged(nameof(Sugar)); }
        }

        public string Fat
        {
            get { return _fat; }
            set { _fat = value; NotifyPropertyChanged(nameof(Fat)); }
        }

        public string Saturated
        {
            get { return _saturated; }
            set { _saturated = value; NotifyPropertyChanged(nameof(Saturated)); }
        }
    }
}
