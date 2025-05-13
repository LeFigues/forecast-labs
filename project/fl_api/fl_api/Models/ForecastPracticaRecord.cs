using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models
{
    public class ForecastPracticaRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("fechaForecast")]
        public DateTime FechaForecast { get; set; } = DateTime.UtcNow;

        [BsonElement("practicaTitulo")]
        public string PracticaTitulo { get; set; } = null!;

        [BsonElement("mes")]
        public string Mes { get; set; } = null!;

        [BsonElement("totalSolicitudes")]
        public int TotalSolicitudes { get; set; }

        [BsonElement("totalEstudiantes")]
        public int TotalEstudiantes { get; set; }
    }
}
