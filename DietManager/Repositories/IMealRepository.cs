using DietManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietManager.Repositories
{
    public interface IMealRepository : IRepository
    {
        Task<List<Meal>> GetAllAsync();
        IMealRepository Add(Meal meal);
        IMealRepository Update(Meal meal);
        IMealRepository Update(IEnumerable<Meal> meals);
        IMealRepository Remove(Meal meal);
    }
}
