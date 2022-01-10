using System;
using System.Collections.ObjectModel;

namespace ArbreSoft.DietManager.Presentation.Models
{
    public class DailyMenu : BindableBase
    {
        public DailyMenu()
        {
            Childrens = new();
        }

        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        
        public ObservableCollection<Meal> Childrens { get; set; }
    }
}
