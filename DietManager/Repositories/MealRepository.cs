using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietManager.Data;
using DietManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DietManager.Repositories
{
    public class MealRepository : IMealRepository
    {
        private AppDbContext context;

        public MealRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IMealRepository Add(Meal meal)
        {
            context.Meals.Add(meal);
            return this;
        }

        public Task<List<Meal>> GetAllAsync()
        {
            return context.Meals.ToListAsync();
        }

        public IMealRepository Update(Meal meal)
        {
            context.Entry(meal).State = EntityState.Modified;
            return this;
        }

        public IMealRepository Update(IEnumerable<Meal> meals)
        {
            meals.ToList().ForEach(m => context.Entry(m).State = EntityState.Modified);
            return this;
        }

        public IMealRepository Remove(Meal meal)
        {
            var ingredientsToRemove = context.Ingredients.Where(i => i.Meal.Id == meal.Id);
            context.Ingredients.RemoveRange(ingredientsToRemove);
            context.Entry(meal).State = EntityState.Deleted;
            return this;
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}