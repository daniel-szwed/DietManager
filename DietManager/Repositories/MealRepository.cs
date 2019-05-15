using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using DietManager.Models;
using DietManager.Services;

namespace DietManager.Repositories
{
    public class MealRepository : IMealRepository
    {
        public Task<int> AddAsync(Meal meal)
        {
            return Task.Run(() => Add(meal));
        }

        private int Add(Meal meal)
        {
            using (var context = new AppDbContext())
            {
                context.Meals.Add(meal);
                return context.SaveChanges();
            }
        }

        public Task<IEnumerable<Meal>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        private IEnumerable<Meal> GetAll()
        {
            using (var context = new AppDbContext())
            {
                return context.Meals.ToList();
            }
        }

        public Task<int> UpdateAsync(Meal meal)
        {
            return Task.Run(() => Update(meal));
        }

        private int Update(Meal meal)
        {
            using (var context = new AppDbContext())
            {
                context.Entry(meal).State = EntityState.Modified;
                try
                {
                    return context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return -1;
                }
            }
        }

        public Task<int> RemoveAsync(Meal meal)
        {
            return Task.Run(() => Remove(meal));
        }

        private int Remove(Meal meal)
        {
            using (var context = new AppDbContext())
            {
                context.Entry(meal).State = EntityState.Deleted;
                try
                {
                    return context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return -1;
                }
            }
        }
    }
}
