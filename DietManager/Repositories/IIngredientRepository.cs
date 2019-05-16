using DietManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietManager.Repositories
{
    public interface IIngredientRepository
    {
        Task<List<Ingredient>> GetAllAsync();
        Task<int> AddAsync(Ingredient ingredient);
        Task<int> UpdateAsync(Ingredient ingredient);
        Task<int> RemoveAsync(Ingredient ingredient);
    }
}
