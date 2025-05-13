using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class Semestre
    {
        [JsonPropertyName("id_semestre")]
        public int IdSemestre { get; set; }

        [JsonPropertyName("numero")]
        public int Numero { get; set; }

        [JsonPropertyName("carrera_nombre")]
        public string CarreraNombre { get; set; } = null!;
    }

}
