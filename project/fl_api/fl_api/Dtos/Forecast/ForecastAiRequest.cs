namespace fl_api.Dtos.Forecast
{
    public class ForecastAiRequest
    {
        public List<ForecastRiesgoDto> Datos { get; set; } = new();
    }
}
