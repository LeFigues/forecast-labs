using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class Materia
    {
        [JsonPropertyName("id_materia")]
        public int IdMateria { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("semestre_numero")]
        public int SemestreNumero { get; set; }

        [JsonPropertyName("carrera_nombre")]
        public string CarreraNombre { get; set; } = null!;
    }

}
