namespace fl_api.Dtos.Forecast
{
    public class ForecastRiesgoDto
    {
        public string InsumoNombre { get; set; } = null!;
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public double UsoMensualPromedio { get; set; }
        public double MesesSobrantes { get; set; }
        public string Riesgo { get; set; } = null!;
    }
}
