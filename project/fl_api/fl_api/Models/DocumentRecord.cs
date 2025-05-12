using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fl_api.Models
{
    public class DocumentRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        public string OriginalFileName { get; set; } = null!;
        public string StoredFileName { get; set; } = null!;
        public DateTime UploadedAt { get; set; }

        public int CareerId { get; set; }
        public string CareerName { get; set; } = null!;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
    }
}
