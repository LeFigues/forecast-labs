using fl_api.Dtos;

namespace fl_api.Interfaces
{
    public interface IStudentsApiClient
    {
        /// <summary>
        /// Llama a GET /api/health en fl_students
        /// </summary>
        Task<HttpResponseMessage> PingAsync();

        /// <summary>
        /// Recupera el catálogo de carreras (y sus materias) desde /api/careers
        /// </summary>
        Task<List<CareerDto>> GetCareersAsync();
    }
}
