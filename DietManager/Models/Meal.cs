using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DietManager.Models
{
    public class Meal : BindableBase
    {
        [Key]
        public int Id { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private ObservableCollection<Ingredient> _ingredients;

        public ObservableCollection<Ingredient> Ingregients
        {
            get { return _ingredients; }
            set { _ingredients = value; }
        }
    }
}
