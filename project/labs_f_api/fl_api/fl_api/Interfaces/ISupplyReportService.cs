using fl_api.Models;

namespace fl_api.Interfaces
{
    public interface ISupplyReportService
    {
        Task<List<SupplyReport>> GetAsync();
        Task<SupplyReport?> GetByIdAsync(string id);
        Task CreateAsync(SupplyReport report);
        Task UpdateAsync(string id, SupplyReport report);
        Task DeleteAsync(string id);
    }
}
