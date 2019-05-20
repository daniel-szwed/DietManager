using System.Threading.Tasks;

namespace DietManager.Repositories
{
    public interface IRepository
    {
        Task<int> SaveChangesAsync();
    }
}
