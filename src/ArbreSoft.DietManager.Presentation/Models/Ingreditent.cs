namespace ArbreSoft.DietManager.Presentation.Models
{
    public class Ingredient : NutritionFact
    {
        private float _amount;

        public Ingredient() { }

        public Ingredient(NutritionFact nutritionFact)
        {
            Kcal = nutritionFact.Kcal;
            Name = nutritionFact.Name;
            Protein = nutritionFact.Protein;
            Carbohydrates = nutritionFact.Carbohydrates;
            Sugar = nutritionFact.Sugar;
            Fat = nutritionFact.Fat;
            Saturated = nutritionFact.Saturated;
        }

        public float Weight
        {
            get { return _amount; }
            set { _amount = value; NotifyPropertyChanged(nameof(Weight)); }
        }
    }
}
