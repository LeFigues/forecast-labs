using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models
{
    public class ForecastRiesgoRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("fecha")]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        [BsonElement("insumoNombre")]
        public string InsumoNombre { get; set; } = null!;

        [BsonElement("stockActual")]
        public int StockActual { get; set; }

        [BsonElement("stockMinimo")]
        public int StockMinimo { get; set; }

        [BsonElement("usoMensualPromedio")]
        public double UsoMensualPromedio { get; set; }

        [BsonElement("mesesSobrantes")]
        public double MesesSobrantes { get; set; }

        [BsonElement("riesgo")]
        public string Riesgo { get; set; } = null!;
    }
}
