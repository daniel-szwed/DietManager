using DietManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietManager.Repositories
{
    public interface IIngredientBaseRepository : IRepository
    {
        Task<List<IngredientBase>> GetAllAsync();
        IIngredientBaseRepository Add(IngredientBase ingredient);
        IIngredientBaseRepository Update(IngredientBase ingredient);
        IIngredientBaseRepository Remove(IngredientBase ingredient);
    }
}
