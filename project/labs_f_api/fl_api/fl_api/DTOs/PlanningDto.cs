using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.DTOs
{
    public class PlanningDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Career { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Classroom { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;
        public string Laboratorio { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public int Grupos { get; set; }
        public List<StudentDto> Students { get; set; } = new();
        public List<MaterialDto> Materials { get; set; } = new();
    }


}
