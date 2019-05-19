using System.Collections.Generic;
using System.Data.Entity;
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

        public Task<List<Meal>> GetAllAsync()
        {
            return _context.Meals.ToListAsync();
        }

        public Task<int> UpdateAsync(Meal meal)
        {
            _context.Entry(meal).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task<int> RemoveAsync(Meal meal)
        {
            Task<int> result = null;
            App.Current.Dispatcher.Invoke(delegate
            {
                _context.Entry(meal).State = EntityState.Deleted;
                var ingredients = _context.Ingredients.Where(i => i.Meal.Id == meal.Id);
                _context.Ingredients.RemoveRange(ingredients);
                result = _context.SaveChangesAsync();
            });
            return result;
        }
    }
}
