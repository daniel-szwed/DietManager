using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Context;
using Data.Repositories.Base;
using DietManager.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class MealRepository : RepositoryBase<Meal>, IMealRepository
    {
        public MealRepository(AppDbContext context): base(context) { }

        public override Task<List<Meal>> Find(Expression<Func<Meal, bool>> predicate)
        {
            return context.Meals.Where(predicate).ToListAsync();
        }

        public override IQueryable<Meal> GetQueryable()
        {
            return context.Meals.AsQueryable();
        }
    }
}