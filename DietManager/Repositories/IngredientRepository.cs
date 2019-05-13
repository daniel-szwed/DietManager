using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DietManager.Models;
using DietManager.Services;

namespace DietManager.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        public IngredientRepository()
        {
        }

        public Task<int> AddAsync(Ingredient ingredient)
        {
            return Task.Run(() => Add(ingredient));
        }

        private int Add(Ingredient ingredient)
        {
            using (var context = new AppDbContext())
            {
                context.Ingredients.Add(ingredient);
                return context.SaveChanges();
            }
        }

        public Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        private IEnumerable<Ingredient> GetAll()
        {
            using (var context = new AppDbContext())
            {
                return context.Ingredients.ToList();
            }
        }

        public Task<int> UpdateAsync(Ingredient ingredient)
        {
            return Task.Run(() => Update(ingredient));
        }

        private int Update(Ingredient ingredient)
        {
            using (var context = new AppDbContext())
            {
                context.Entry(ingredient).State = EntityState.Modified;
                return context.SaveChanges();
            }
        }

        public Task<int> RemoveAsync(Ingredient ingredient)
        {
            return Task.Run(() => Remove(ingredient));
        }

        private int Remove(Ingredient ingredient)
        {
            using (var context = new AppDbContext())
            {
                context.Entry(ingredient).State = EntityState.Deleted;
                return context.SaveChanges();
            }
        }
    }
}
