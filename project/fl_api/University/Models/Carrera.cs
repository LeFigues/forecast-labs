using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class Carrera
    {
        [JsonPropertyName("id_carrera")]
        public int IdCarrera { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("siglas")]
        public string Siglas { get; set; } = null!;
    }

}
