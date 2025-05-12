using fl_front.Models;

namespace fl_front.Services
{
    public interface IHealthService
    {
        Task<HealthCheckResponse?> GetHealthStatusAsync();
    }
}
