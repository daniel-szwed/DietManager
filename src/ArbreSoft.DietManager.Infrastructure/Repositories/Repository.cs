using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
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

        public Task<int> ExecuteNonQuery(string query, params IDbParameter[] parameters)
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

    public static class DbSetExtensions
    {
        /// <summary>
        /// Ensures that all navigation properties (up to a certain depth) are eagerly loaded when entities are resolved from this
        /// DbSet.
        /// </summary>
        /// <returns>The queryable representation of this DbSet</returns>
        public static IQueryable<TEntity> IncludeAll<TEntity>(
            this DbSet<TEntity> dbSet,
            int maxDepth = int.MaxValue) where TEntity : class
        {
            IQueryable<TEntity> result = dbSet;
            var context = dbSet.GetService<ICurrentDbContext>().Context;
            var includePaths = GetIncludePaths<TEntity>(context, maxDepth);

            foreach (var includePath in includePaths)
            {
                result = result.Include(includePath);
            }

            return result;
        }

        /// <remarks>
        /// Adapted from https://stackoverflow.com/a/49597502/1636276
        /// </remarks>
        private static IEnumerable<string> GetIncludePaths<T>(DbContext context, int maxDepth = int.MaxValue)
        {
            if (maxDepth < 0)
                throw new ArgumentOutOfRangeException(nameof(maxDepth));

            var entityType = context.Model.FindEntityType(typeof(T));
            var includedNavigations = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();

            while (true)
            {
                var entityNavigations = new List<INavigation>();

                if (stack.Count <= maxDepth)
                {
                    foreach (var navigation in entityType.GetNavigations())
                    {
                        if (includedNavigations.Add(navigation))
                            entityNavigations.Add(navigation);
                    }
                }

                if (entityNavigations.Count == 0)
                {
                    if (stack.Count > 0)
                        yield return string.Join(".", stack.Reverse().Select(e => e.Current!.Name));
                }
                else
                {
                    foreach (var navigation in entityNavigations)
                    {
                        var inverseNavigation = navigation.FindInverse();
                        if (inverseNavigation != null)
                            includedNavigations.Add(inverseNavigation);
                    }

                    stack.Push(entityNavigations.GetEnumerator());
                }

                while (stack.Count > 0 && !stack.Peek().MoveNext())
                    stack.Pop();

                if (stack.Count == 0)
                    break;

                entityType = stack.Peek().Current!.GetTargetType();
            }
        }
    }
}

