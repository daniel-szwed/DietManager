using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Presentation.Models
{
    public class Meal : BindableBase
    {
        private string _name;

        public Meal()
        {
            Ingregients = new ObservableCollection<Ingredient>();
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
