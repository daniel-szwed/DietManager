using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<string, dynamic> repositories;
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public Task<ITransaction> BeginTransactionAsync()
        {
            var transaction = context.Database.BeginTransaction();

            return Task.FromResult(transaction as ITransaction);
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
        {
            if (repositories == null)
                repositories = new Dictionary<string, dynamic>();

            var type = typeof(TEntity).Name;

            if (repositories.ContainsKey(type))
                return (IRepository<TEntity>)repositories[type]; 
            
            var repositoryType = typeof(Repository<>);

            repositories.Add(
                type,
                Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(TEntity)),
                    context)
            );

            return repositories[type];
        }

        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
    }
}
