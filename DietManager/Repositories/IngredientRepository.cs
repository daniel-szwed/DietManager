using DietManager.DataLayer;
using DietManager.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DietManager.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private AppDbContext _context;

        public IngredientRepository()
        {
            _context = DbSession.Instance.GetAppDbcontext();
        }

        public IIngredientRepository Add(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            return this;
        }

        public Task<List<Ingredient>> GetAllAsync()
        {
            return _context.Ingredients.ToListAsync();
        }

        public IIngredientRepository Update(Ingredient ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Modified;
            return this;
        }

        public IIngredientRepository Remove(Ingredient ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Deleted;
            return this;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
