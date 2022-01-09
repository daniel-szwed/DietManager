using System;

namespace ArbreSoft.DietManager.Domain
{
    public class Meal : Ingredient
    {
        public TimeSpan Time { get; set; }

        public override NutritionFact Sum()
        {
            var result = new NutritionFact();

            Childrens.ForEach(ingredient =>
            {
                result += ingredient.Sum();
            });

            return result;
        }
    }
}
