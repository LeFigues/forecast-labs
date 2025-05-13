namespace fl_api.Dtos.Forecast
{
    public class ForecastHistoricoDto
    {
        public string InsumoNombre { get; set; } = null!;
        public string Mes { get; set; } = null!; // "2025-05"
        public int TotalUsado { get; set; }
    }
}
