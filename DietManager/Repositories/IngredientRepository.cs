using System;
using System.Collections.Generic;
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

        public Task<int> AddIngredientAsync(Ingredient ingredient)
        {
            return Task.Run(() => AddIngredient(ingredient));
        }

        private int AddIngredient(Ingredient ingredient)
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
    }
}
