using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class Alerta
    {
        [JsonPropertyName("id_alerta")]
        public int IdAlerta { get; set; }

        [JsonPropertyName("mensaje")]
        public string Mensaje { get; set; } = null!;

        [JsonPropertyName("tipo")]
        public string? Tipo { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; } = null!;

        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; }

        [JsonPropertyName("insumo_nombre")]
        public string InsumoNombre { get; set; } = null!;

        [JsonPropertyName("stock_actual")]
        public int StockActual { get; set; }

        [JsonPropertyName("stock_minimo")]
        public int StockMinimo { get; set; }

        [JsonPropertyName("stock_maximo")]
        public int StockMaximo { get; set; }
    }

}
