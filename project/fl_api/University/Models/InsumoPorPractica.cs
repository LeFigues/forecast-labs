using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class InsumoPorPractica
    {
        [JsonPropertyName("id_practica")]
        public int IdPractica { get; set; }

        [JsonPropertyName("nombre_insumo")]
        public string NombreInsumo { get; set; } = null!;

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }
    }
}
