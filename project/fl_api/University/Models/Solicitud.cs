using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class Solicitud
    {
        [JsonPropertyName("id_solicitud")]
        public int IdSolicitud { get; set; }

        [JsonPropertyName("id_insumo")]
        public int? IdInsumo { get; set; }

        [JsonPropertyName("cantidad_solicitada")]
        public int CantidadSolicitada { get; set; }

        [JsonPropertyName("fecha_solicitud")]
        public DateTime FechaSolicitud { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; } = null!;

        [JsonPropertyName("observaciones")]
        public string Observaciones { get; set; } = null!;

        [JsonPropertyName("nombre_solicitud")]
        public string NombreSolicitud { get; set; } = null!;
    }

}
