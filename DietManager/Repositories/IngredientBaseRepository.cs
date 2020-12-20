using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietManager.Data;
using DietManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DietManager.Repositories
{
    public class IngredientBaseRepository : IIngredientBaseRepository
    {
        private AppDbContext context;

        public IngredientBaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IIngredientBaseRepository Add(IngredientBase ingredient)
        {
            context.IngredientBase.Add(ingredient);
            return this;
        }

        public Task<List<IngredientBase>> GetAllAsync()
        {
            return context.IngredientBase.Where(i => !(i is Ingredient)).ToListAsync();
        }

        public IIngredientBaseRepository Update(IngredientBase ingredient)
        {
            context.Entry(ingredient).State = EntityState.Modified;
            return this;
        }

        public IIngredientBaseRepository Remove(IngredientBase ingredient)
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
