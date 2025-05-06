namespace fl_api.DTOs
{
    public class Material
    {
        public int Cantidad_Por_Grupo { get; set; }

        public string Unidad { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? Observaciones { get; set; }
    }
}
