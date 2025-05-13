using fl_api.Configurations;
using fl_api.Dtos;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;

namespace fl_api.Services
{
    public class LabsApiClient : ILabsApiClient
    {
        private readonly HttpClient _http;

        public LabsApiClient(HttpClient http, IOptions<ApiLabsSettings> options)
        {
            _http = http;
            _http.BaseAddress = new Uri(options.Value.BaseUrl);
        }

        public async Task<LabInfoDto> GetLabAsync(string id)
            => await _http.GetFromJsonAsync<LabInfoDto>("/carreras/1");
    }
}
