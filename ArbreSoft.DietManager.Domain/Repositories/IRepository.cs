using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> GetManyAsync(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> FromSqlRawAsync(string query,
                                                string includeProperties,
                                                params IDbParameter[] parameters);

        Task ExecuteNonQuery(string query, params IDbParameter[] parameters);
    }
}
