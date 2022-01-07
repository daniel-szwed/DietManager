using ArbreSoft.DietManager.Presentation.Models;
using System.Threading.Tasks;

namespace DietManager.Services
{
    public interface IIngredientService
    {
        Task<NutritionFact> SearchIngredientAsync(string name);
    }
}
