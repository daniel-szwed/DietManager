using DietManager.Models;
using System.Threading.Tasks;

namespace DietManager.Services
{
    public interface IIngredientService
    {
        Task<Ingredient> SearchIngredientAsync(string name);
    }
}
