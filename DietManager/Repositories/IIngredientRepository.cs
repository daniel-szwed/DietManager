using DietManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietManager.Repositories
{
    public interface IIngredientRepository : IRepository
    {
        Task<List<Ingredient>> GetAllAsync();
        IIngredientRepository Add(Ingredient ingredient);
        IIngredientRepository Update(Ingredient ingredient);
        IIngredientRepository Remove(Ingredient ingredient);
    }
}
