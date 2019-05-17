using System.Collections.Generic;
using System.Linq;
using DietManager.Models;

namespace DietManager.Services
{
    public class MealService : IMealService
    {
        private IIngredientService _ingredientService;

        public MealService(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public IngredientBase GetSum(IEnumerable<Meal> meals)
        {
            var mealSubResult = new List<IngredientBase>();
            meals.ToList().ForEach(m => {
                var sumIngr = _ingredientService.GetSum(m.Ingregients);
                mealSubResult.Add(_ingredientService.GetSum(m.Ingregients));
            });
            return _ingredientService.GetSum(mealSubResult);
        }
    }
}