using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class Docente
    {
        [JsonPropertyName("id_docente")]
        public int IdDocente { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("apellido")]
        public string Apellido { get; set; } = null!;

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = null!;

        [JsonPropertyName("id_carrera")]
        public int IdCarrera { get; set; }
    }

}
