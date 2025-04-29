using fl_api.DTOs;

namespace fl_api.Interfaces
{
    public interface IPlanningRepository
    {
        Task<List<PlanningDto>> GetAllAsync();
        Task CreateAsync(PlanningDto planning);
    }
}
