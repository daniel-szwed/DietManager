using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using DietManager.DataLayer;
using DietManager.Models;

namespace DietManager.Repositories
{
    public class MealRepository : IMealRepository
    {
        private AppDbContext _context;

        public MealRepository()
        {
            _context = DbSession.Instance.GetAppDbcontext();
        }
        public Task<int> AddAsync(Meal meal)
        {
            return Task.Run(() => Add(meal));
        }

        private int Add(Meal meal)
        {
            _context.Meals.Add(meal);
            return _context.SaveChanges();
        }

        public Task<IEnumerable<Meal>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        private IEnumerable<Meal> GetAll()
        {
            return _context.Meals.ToList();
        }

        public Task<int> UpdateAsync(Meal meal)
        {
            return Task.Run(() => Update(meal));
        }

        private int Update(Meal meal)
        {
            _context.Entry(meal).State = EntityState.Modified;
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }

        public Task<int> RemoveAsync(Meal meal)
        {
            return Task.Run(() => Remove(meal));
        }

        private int Remove(Meal meal)
        {
            _context.Entry(meal).State = EntityState.Deleted;
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
