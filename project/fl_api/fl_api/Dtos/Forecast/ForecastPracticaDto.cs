namespace fl_api.Dtos.Forecast
{
    public class ForecastPracticaDto
    {
        public string PracticaTitulo { get; set; } = null!;
        public string Mes { get; set; } = null!;
        public int TotalSolicitudes { get; set; }
        public int TotalEstudiantes { get; set; }
    }
}
