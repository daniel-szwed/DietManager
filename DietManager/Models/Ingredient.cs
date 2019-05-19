using System.ComponentModel.DataAnnotations;

namespace DietManager.Models
{
    public class Ingredient : IngredientBase
    {
        private float _amount;

        public Ingredient() { }

        public Ingredient(IngredientBase ingredientBase)
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

        public virtual Meal Meal { get; set; }
    }
}