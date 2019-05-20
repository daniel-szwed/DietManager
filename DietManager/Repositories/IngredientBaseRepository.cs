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

        public IIngredientBaseRepository Add(IngredientBase ingredient)
        {
            _context.IngredientBase.Add(ingredient);
            return this;
        }

        public Task<List<IngredientBase>> GetAllAsync()
        {
            return _context.IngredientBase.Where(i => !(i is Ingredient)).ToListAsync();
        }

        public IIngredientBaseRepository Update(IngredientBase ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Modified;
            return this;
        }

        public IIngredientBaseRepository Remove(IngredientBase ingredient)
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
