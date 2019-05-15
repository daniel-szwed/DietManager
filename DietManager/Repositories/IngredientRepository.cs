using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using DietManager.DataLayer;
using DietManager.Models;

namespace DietManager.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private AppDbContext _context;

        public IngredientRepository()
        {
            _context = DbSession.Instance.GetAppDbcontext();
        }
        public Task<int> AddAsync(Ingredient ingredient)
        {
            return Task.Run(() => Add(ingredient));
        }

        private int Add(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            return _context.SaveChanges();
        }

        public Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        private IEnumerable<Ingredient> GetAll()
        {
            return _context.Ingredients.ToList();
        }

        public Task<int> UpdateAsync(Ingredient ingredient)
        {
            return Task.Run(() => Update(ingredient));
        }

        private int Update(Ingredient ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Modified;
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }

        public Task<int> RemoveAsync(Ingredient ingredient)
        {
            return Task.Run(() => Remove(ingredient));
        }

        private int Remove(Ingredient ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Deleted;
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }
    }
}
