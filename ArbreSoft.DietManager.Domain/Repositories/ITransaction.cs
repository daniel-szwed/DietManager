using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Domain.Repositories
{
    public interface ITransaction
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}