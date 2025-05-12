using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fl_api.Models
{
    public class PlanningRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid DocumentId { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Group { get; set; } = null!;
        public string Classroom { get; set; } = null!;
        public string Teacher { get; set; } = null!;
        public string LabCode { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int Groups { get; set; }

        public List<Student> Students { get; set; } = new();

        // Aquí embebemos el análisis que ya existe en 'openai'
        public BsonDocument AnalysisResult { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
