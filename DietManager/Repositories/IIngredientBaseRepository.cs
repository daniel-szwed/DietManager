using DietManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietManager.Repositories
{
    public interface IIngredientBaseRepository
    {
        Task<List<IngredientBase>> GetAllAsync();
        Task<int> AddAsync(IngredientBase ingredient);
        Task<int> UpdateAsync(IngredientBase ingredient);
        Task<int> RemoveAsync(IngredientBase ingredient);
    }
}
