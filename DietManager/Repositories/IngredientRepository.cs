using DietManager.Data;
using DietManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietManager.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private AppDbContext context;

        public IngredientRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IIngredientRepository Add(Ingredient ingredient)
        {
            context.Ingredients.Add(ingredient);
            return this;
        }

        public Task<List<Ingredient>> GetAllAsync()
        {
            return context.Ingredients.ToListAsync();
        }

        public IIngredientRepository Update(Ingredient ingredient)
        {
            context.Entry(ingredient).State = EntityState.Modified;
            return this;
        }

        public IIngredientRepository Remove(Ingredient ingredient)
        {
            context.Entry(ingredient).State = EntityState.Deleted;
            return this;
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
