using System.Collections.Generic;

namespace ArbreSoft.DietManager.Domain
{
    public class NutritionFact : EntityBase
    {
        public string Name { get; set; }
        public decimal KiloCalories { get; set; }
        public decimal Proteins { get; set; }
        public decimal TotalCarbohydreates { get; set; }
        public decimal Sugars { get; set; }
        public decimal TotalFats { get; set; }
        public decimal SaturatedFats { get; set; }

        public virtual List<Ingredient> Childrens { get; set; }
        public virtual void Add(Ingredient ingredient) => Childrens.Add(ingredient);
        public virtual void Remove(Ingredient ingredient) => Childrens.Remove(ingredient);

        public virtual NutritionFact Sum()
        {
            var result = new NutritionFact();

            foreach (var propertyInfo in typeof(NutritionFact).GetProperties())
            {
                if (propertyInfo.GetValue(this) is decimal value)
                {
                    propertyInfo.SetValue(result, value / 100);
                }
            }

            return result;
        }

        public static NutritionFact operator +(NutritionFact a, NutritionFact b)
        {
            var result = new NutritionFact();

            foreach (var propertyInfo in typeof(NutritionFact).GetProperties())
            {
                if (propertyInfo.GetValue(a) is decimal aValue 
                    && propertyInfo.GetValue(b) is decimal bValue)
                {
                    propertyInfo.SetValue(result, aValue + bValue);
                }
            }

            return result;
        }

        public static NutritionFact operator *(NutritionFact a, decimal b)
        {
            var result = new NutritionFact();

            foreach (var propertyInfo in typeof(NutritionFact).GetProperties())
            {
                if (propertyInfo.GetValue(a) is decimal aValue)
                {
                    propertyInfo.SetValue(result, aValue * b);
                }
            }

            return result;
        }
    }
}
