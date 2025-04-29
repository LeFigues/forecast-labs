using System.Net.Http.Json;
using FL_Front_Web_SIN.Models;

namespace FL_Front_Web_SIN.Services
{
    public class Services_ForecastService
    {
        private readonly IHttpClientFactory _clientFactory;

        public Services_ForecastService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<SupplyReport>> GetSupplyReportAsync()
        {
            var client = _clientFactory.CreateClient("ForecastAPI");

            var response = await client.GetFromJsonAsync<List<ApiSupplyReport>>("report/supplies");

            if (response == null)
                return new List<SupplyReport>();

            return response.Select(item => new SupplyReport
            {
                Producto = item.name,
                Usada = item.quantityUsed,
                Danada = item.quantityDamaged,
                Consumida = item.quantityConsumed,
                Requerida = item.quantityRequired
            }).ToList();
        }
    }

    // Clase para mapear la respuesta real de la API
    public class ApiSupplyReport
    {
        public string name { get; set; }
        public int quantityUsed { get; set; }
        public int quantityDamaged { get; set; }
        public int quantityConsumed { get; set; }
        public int quantityRequired { get; set; }
    }
}
