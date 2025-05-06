namespace fl_api.DTOs
{
    public class MaterialDto
    {
        public int CantidadPorGrupo { get; set; }
        public string Unidad { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Observations { get; set; }
    }

}
