using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class Laboratorio
    {
        [JsonPropertyName("id_laboratorio")]
        public int IdLaboratorio { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("ubicacion")]
        public string? Ubicacion { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("id_encargado")]
        public int IdEncargado { get; set; }
    }

}
