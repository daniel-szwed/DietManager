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
    public class IngredientBaseRepository : RepositoryBase<IngredientBase>, IIngredientBaseRepository
    {
        public IngredientBaseRepository(AppDbContext context): base(context) { }

        public override Task<List<IngredientBase>> Find(Expression<Func<IngredientBase, bool>> predicate)
        {
            return context.IngredientBase.Where(predicate).ToListAsync();
        }

        public override IQueryable<IngredientBase> GetQueryable()
        {
            return context.IngredientBase.AsQueryable();
        }
    }
}
