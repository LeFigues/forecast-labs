using fl_api.Dtos;

namespace fl_api.Interfaces
{
    public interface ILabsApiClient
    {
        Task<LabInfoDto> GetLabAsync(string id);
        // Otros métodos según contrato
    }
}
