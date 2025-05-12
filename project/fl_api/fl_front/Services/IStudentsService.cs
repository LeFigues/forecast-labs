using fl_front.Models;

namespace fl_front.Services
{
    public interface IStudentsService
    {
        Task<List<Career>> GetCareersAsync();
    }
}
