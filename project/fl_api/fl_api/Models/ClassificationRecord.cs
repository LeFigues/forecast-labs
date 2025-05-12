using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models
{
    public class ClassificationRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        // --> Aquí indicamos que el Guid se serialice como String en Mongo
        [BsonRepresentation(BsonType.String)]
        public Guid DocumentId { get; set; }

        public int CareerId { get; set; }
        public string CareerName { get; set; } = null!;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public DateTime ClassifiedAt { get; set; }
    }
}
