using DietManager.Models;
using System.Threading.Tasks;

namespace DietManager.Services
{
    public interface IIngredientService
    {
        Task<IngredientBase> SearchIngredientAsync(string name);
    }
}
