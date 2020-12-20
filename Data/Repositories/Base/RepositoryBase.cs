using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories.Base
{
    public abstract class RepositoryBase<TModel> : IRepository<TModel> where TModel : class
    {
        protected AppDbContext context;

        public RepositoryBase(AppDbContext context)
        {
            this.context = context;
        }

        public virtual void Add(TModel model)
        {
            context.Add(model);
        }

        public virtual void AddRange(IEnumerable<TModel> models)
        {
            context.AddRange(models);
        }

        public Task<IDbContextTransaction> BeginTransaction()
        {
            return context.Database.BeginTransactionAsync();
        }

        public virtual Task<List<TModel>> Find(Expression<Func<TModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TModel> GetQueryable()
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(TModel model)
        {
            context.Remove(model);
        }

        public virtual Task<int> SaveChangesAsync()
        {
            Task<int> result = new Task<int>(() => -1);
            try
            {
                result = context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO: log this exception
            }
            return result;
        }

        public virtual void Update(TModel model)
        {
            context.Entry(model).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TModel> models)
        {
            models.ToList().ForEach(m => Update(m));
        }
    }
}
