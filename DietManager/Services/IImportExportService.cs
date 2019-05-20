using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietManager.Services
{
    public interface IImportExportService
    {
        void ExportAsync<T>(IEnumerable<T> collection);
        Task<IEnumerable<T>> ImportAsync<T>();
    }
}
