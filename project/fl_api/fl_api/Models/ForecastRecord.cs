using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models
{
    public class ForecastRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Item { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public DateTime PeriodStart { get; set; }  // primer día del mes o semestre
        public DateTime PeriodEnd { get; set; }  // último día

        public double ForecastQty { get; set; }
        public string Model { get; set; } = "linear-regression";
        public DateTime GeneratedAt { get; set; }
    }
}
