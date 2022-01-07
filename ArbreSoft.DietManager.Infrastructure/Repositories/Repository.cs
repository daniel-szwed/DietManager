using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly AppDbContext context;
        private readonly DbSet<TEntity> dbSet;

        public Repository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Added;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AttachRange(entities);
            context.Entry(entities).State = EntityState.Added;
        }

        public Task ExecuteNonQuery(string query, params IDbParameter[] parameters)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                context.Database.OpenConnection();
                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = param.ParameterName;
                    parameter.Value = param.Value;
                    command.Parameters.Add(parameter);
                }

                return command.ExecuteNonQueryAsync();
            }
        }

        public Task<IEnumerable<TEntity>> FromSqlRawAsync(string query, string includeProperties, params IDbParameter[] parameters)
        {
            var sqliteParameters = new List<SqliteParameter>();
            foreach (var param in parameters)
            {
                sqliteParameters.Add(new SqliteParameter(param.ParameterName, param.Value));
            }

            IQueryable<TEntity> queryable = dbSet.FromSqlRaw(query, sqliteParameters.ToArray());

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                queryable = queryable.Include(includeProperty);
            }

            return Task.FromResult(queryable.AsEnumerable());
        }

        public Task<IEnumerable<TEntity>> GetManyAsync(ISpecification<TEntity> specification)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.AttachRange(entities);
            context.Entry(entities).State = EntityState.Deleted;
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.AttachRange(entities);
            context.Entry(entities).State = EntityState.Modified;
        }
    }
}
