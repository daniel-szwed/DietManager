using System;
using System.Linq;
using System.Linq.Expressions;

namespace ArbreSoft.DietManager.Domain.Repositories
{
    public interface ISpecification<TEntity> where TEntity : EntityBase
    {
        public Expression<Func<TEntity, bool>> Predicate { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }
        public string IncludeProperties { get; set; }
    }
}
