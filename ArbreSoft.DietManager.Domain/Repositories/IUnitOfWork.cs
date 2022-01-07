using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<ITransaction> BeginTransactionAsync();
        IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;
        Task<int> SaveChangesAsync();
    }
}
