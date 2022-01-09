using System;
using System.Collections.ObjectModel;

namespace ArbreSoft.DietManager.Presentation.Models
{
    public class Meal : BindableBase
    {
        private string _name;

        public Meal()
        {
            Ingregients = new ObservableCollection<Ingredient>();
        }

        public Meal(string name) : this()
        {
            Name = name;
        }

        public Guid Id { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public virtual ObservableCollection<Ingredient> Ingregients { get; set; }
    }
}
