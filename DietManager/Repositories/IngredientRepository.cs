﻿using DietManager.DataLayer;
using DietManager.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DietManager.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private AppDbContext _context;

        public IngredientRepository()
        {
            _context = DbSession.Instance.GetAppDbcontext();
        }

        public Task<int> AddAsync(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            return _context.SaveChangesAsync();
        }

        public Task<List<Ingredient>> GetAllAsync()
        {
            return _context.Ingredients.ToListAsync();
        }

        public Task<int> UpdateAsync(Ingredient ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task<int> RemoveAsync(Ingredient ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Deleted;
            return _context.SaveChangesAsync();
        }

    }
}
