using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json;

namespace fl_api.Models
{
    public class PythonAnalysisRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("DocumentId")]
        [BsonRepresentation(BsonType.String)]
        public Guid DocumentId { get; set; }

        public string FileName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Version { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public int PracticeNumber { get; set; }
        public string Title { get; set; } = null!;
        public int Groups { get; set; }

        // Sólo guardamos aquí la estructura _mejorada_ por OpenAI
        public BsonDocument AnalysisResult { get; set; } = null!;

        public string ModelUsed { get; set; } = null!;
        public DateTime AnalyzedAt { get; set; }
        public double SatisfactionPercentage { get; set; }
    }
}
