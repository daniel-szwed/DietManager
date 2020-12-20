using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories.Base
{
    public interface IRepository<TModel>
    {
        void Add(TModel model);
        void AddRange(IEnumerable<TModel> models);
        Task<List<TModel>> Find(Expression<Func<TModel, bool>> predicate);
        IQueryable<TModel> GetQueryable();
        void Update(TModel model);
        void UpdateRange(IEnumerable<TModel> models);
        void Remove(TModel model);
        Task<IDbContextTransaction> BeginTransaction();
        Task<int> SaveChangesAsync();
    }
}
