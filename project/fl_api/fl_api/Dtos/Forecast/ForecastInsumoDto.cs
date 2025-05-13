namespace fl_api.Dtos.Forecast
{
    public class ForecastInsumoDto
    {
        public string InsumoNombre { get; set; } = null!;
        public string PracticaTitulo { get; set; } = null!;
        public int CantidadRequerida { get; set; }
    }
}
