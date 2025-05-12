using fl_api.Dtos;
using fl_api.Models;

namespace fl_api.Interfaces
{
    public interface IPlanningService
    {
        Task<PlanningRecord> CreatePlanningAsync(Guid documentId, PlanningCreateDto dto);
        Task<PlanningRecord?> GetPlanningAsync(string planningId);
        Task<List<PlanningRecord>> GetAllPlanningsAsync();
        Task<PlanningRecord?> UpdatePlanningAsync(string planningId, PlanningUpdateDto dto);
        Task<bool> DeletePlanningAsync(string planningId);
    }
}
