using Data.Context;
using Data.Repositories.Base;
using DietManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class IngredientRepository : RepositoryBase<Ingredient>, IIngredientRepository 
    {
        public IngredientRepository(AppDbContext context): base(context) { }

        public override Task<List<Ingredient>> Find(Expression<Func<Ingredient, bool>> predicate)
        {
            return context.Ingredients.Where(predicate).ToListAsync();
        }

        public override IQueryable<Ingredient> GetQueryable()
        {
            return context.Ingredients.AsQueryable();
        }
    }
}
