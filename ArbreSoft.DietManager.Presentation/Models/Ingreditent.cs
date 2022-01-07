using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Presentation.Models
{
    public class Ingredient : NutritionFact
    {
        private float _amount;

        public Ingredient() { }

        public Ingredient(NutritionFact ingredientBase)
        {
            Kcal = ingredientBase.Kcal;
            Name = ingredientBase.Name;
            Protein = ingredientBase.Protein;
            Carbohydrates = ingredientBase.Carbohydrates;
            Sugar = ingredientBase.Sugar;
            Fat = ingredientBase.Fat;
            Saturated = ingredientBase.Saturated;
        }

        public float Amount
        {
            get { return _amount; }
            set { _amount = value; NotifyPropertyChanged(nameof(Amount)); }
        }

        //[JsonIgnore]
        public virtual Meal Meal { get; set; }
    }
}
