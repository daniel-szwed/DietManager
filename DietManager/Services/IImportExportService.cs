using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietManager.Services
{
    public interface IImportExportService
    {
        Task<IEnumerable<T>> ImportAsync<T>();
        void ExportAsync<T>(IEnumerable<T> collection);
    }
}
