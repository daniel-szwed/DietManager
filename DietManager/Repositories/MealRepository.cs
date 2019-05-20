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

        public IMealRepository Add(Meal meal)
        {
            _context.Meals.Add(meal);
            return this;
        }

        public Task<List<Meal>> GetAllAsync()
        {
            return _context.Meals.ToListAsync();
        }

        public IMealRepository Update(Meal meal)
        {
            _context.Entry(meal).State = EntityState.Modified;
            return this;
        }

        public IMealRepository Update(IEnumerable<Meal> meals)
        {
            meals.ToList().ForEach(m => _context.Entry(m).State = EntityState.Modified);
            return this;
        }

        public IMealRepository Remove(Meal meal)
        {
            var ingredientsToRemove = _context.Ingredients.Where(i => i.Meal.Id == meal.Id);
            _context.Ingredients.RemoveRange(ingredientsToRemove);
            _context.Entry(meal).State = EntityState.Deleted;
            return this;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}