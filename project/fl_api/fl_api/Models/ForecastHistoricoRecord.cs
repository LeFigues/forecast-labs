using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models
{
    public class ForecastHistoricoRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("fechaForecast")]
        public DateTime FechaForecast { get; set; } = DateTime.UtcNow;

        [BsonElement("insumoNombre")]
        public string InsumoNombre { get; set; } = null!;

        [BsonElement("mes")]
        public string Mes { get; set; } = null!;

        [BsonElement("totalUsado")]
        public int TotalUsado { get; set; }
    }
}
