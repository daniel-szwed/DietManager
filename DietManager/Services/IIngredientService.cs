using DietManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietManager.Services
{
    public interface IIngredientService
    {
        Task<IngredientBase> SearchIngredientAsync(string name);
        IngredientBase GetSum(IEnumerable<Ingredient> ingregients);
        IngredientBase GetSum(IEnumerable<IngredientBase> ingregients);
    }
}
