using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DietManager.DataLayer;
using DietManager.Models;

namespace DietManager.Repositories
{
    public class IngredientBaseRepository : IIngredientBaseRepository
    {
        private AppDbContext _context;

        public IngredientBaseRepository()
        {
            _context = DbSession.Instance.GetAppDbcontext();
        }

        public Task<int> AddAsync(IngredientBase ingredient)
        {
            _context.IngredientBase.Add(ingredient);
            return _context.SaveChangesAsync();
        }

        public Task<List<IngredientBase>> GetAllAsync()
        {
            return _context.IngredientBase.Where(i => !(i is Ingredient)).ToListAsync();
        }

        public Task<int> UpdateAsync(IngredientBase ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task<int> RemoveAsync(IngredientBase ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Deleted;
            return _context.SaveChangesAsync();
        }
    }
}
