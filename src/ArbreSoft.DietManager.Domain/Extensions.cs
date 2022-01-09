using System;

namespace ArbreSoft.DietManager.Domain
{
    public static class Extensions
    {
        public static NutritionFact ToFixed(this NutritionFact nutritionFact, int round)
        {
            var result = new NutritionFact();

            foreach (var propertyInfo in typeof(NutritionFact).GetProperties())
            {
                if (propertyInfo.GetValue(nutritionFact) is decimal propertyValue)
                {
                    propertyInfo.SetValue(result, Math.Round(propertyValue, round, MidpointRounding.AwayFromZero));
                }
            }

            return result;
        }
    }
}
