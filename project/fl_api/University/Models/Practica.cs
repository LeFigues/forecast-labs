using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class Practica
    {
        [JsonPropertyName("id_practica")]
        public int IdPractica { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; } = null!;

        [JsonPropertyName("numero_practica")]
        public int NumeroPractica { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = null!;

        [JsonPropertyName("id_laboratorio")]
        public int IdLaboratorio { get; set; }

        [JsonPropertyName("id_materia")]
        public int IdMateria { get; set; }
    }

}
