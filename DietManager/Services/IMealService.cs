using DietManager.Models;
using System.Collections.Generic;

namespace DietManager.Services
{
    public interface IMealService
    {
        IngredientBase GetSum(IEnumerable<Meal> meals);
    }
}
