using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class Insumo
    {
        [JsonPropertyName("id_insumo")]
        public int IdInsumo { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = null!;

        [JsonPropertyName("ubicacion")]
        public string Ubicacion { get; set; } = null!;

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; } = null!;

        [JsonPropertyName("unidad_medida")]
        public string UnidadMedida { get; set; } = null!;

        [JsonPropertyName("stock_actual")]
        public int StockActual { get; set; }

        [JsonPropertyName("stock_minimo")]
        public int StockMinimo { get; set; }

        [JsonPropertyName("stock_maximo")]
        public int StockMaximo { get; set; }
    }

}
