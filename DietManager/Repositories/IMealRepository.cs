using DietManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietManager.Repositories
{
    public interface IMealRepository
    {
        Task<List<Meal>> GetAllAsync();
        Task<int> AddAsync(Meal meal);
        Task<int> UpdateAsync(Meal meal);
        Task<int> RemoveAsync(Meal meal);
    }
}
