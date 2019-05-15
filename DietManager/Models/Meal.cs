using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DietManager.Models
{
    public class Meal : BindableBase
    {
        private string _name;

        public Meal()
        {
            Ingregients = new ObservableCollection<Ingredient>();
        }

        [Key]
        public int Id { get; set; }
        
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public virtual ICollection<Ingredient> Ingregients { get; set; }
    }
}
