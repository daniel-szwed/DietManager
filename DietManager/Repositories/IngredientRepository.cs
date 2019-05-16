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
        public Task<int> AddAsync(IngredientBase ingredient)
        {
            return Task.Run(() => Add(ingredient));
        }

        private int Add(IngredientBase ingredient)
        {
            _context.IngredientBase.Add(ingredient);
            return _context.SaveChanges();
        }

        public Task<IEnumerable<IngredientBase>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        private IEnumerable<IngredientBase> GetAll()
        {
            return _context.IngredientBase.ToList();
        }

        public Task<int> UpdateAsync(IngredientBase ingredient)
        {
            return Task.Run(() => Update(ingredient));
        }

        private int Update(IngredientBase ingredient)
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

        public Task<int> RemoveAsync(IngredientBase ingredient)
        {
            return Task.Run(() => Remove(ingredient));
        }

        private int Remove(IngredientBase ingredient)
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
