namespace ArbreSoft.DietManager.Domain
{
    public class Ingredient : NutritionFact
    {
        public Ingredient() 
        {
        }

        public Ingredient(NutritionFact source, decimal weight)
        {
            foreach (var propertyInfo in typeof(NutritionFact).GetProperties())
            {
                if (propertyInfo.GetValue(source) is decimal value)
                {
                    propertyInfo.SetValue(this, value);
                }
            }

            Weight = weight;
        }

        public decimal Weight { get; set; }

        public void Increase() => Weight++;
        public void Decrease() => Weight--;

        public override NutritionFact Sum()
        {
            var result = base.Sum();

            foreach (var propertyInfo in typeof(NutritionFact).GetProperties())
            {
                if (propertyInfo.GetValue(result) is decimal value)
                {
                    propertyInfo.SetValue(result, value * Weight);
                }
            }

            return result;
        }
    }
}
