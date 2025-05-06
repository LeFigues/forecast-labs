using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.DTOs
{
    public class LabAnalysisResult
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("laboratorio")]
        public string Laboratorio { get; set; } = null!;

        [BsonElement("titulo")]
        public string Titulo { get; set; } = null!;

        [BsonElement("grupos")]
        public int Grupos { get; set; }

        [BsonElement("materiales")]
        public Materiales Materiales { get; set; } = null!;
    }
}
