using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models
{
    public class PromptDefinition
    {
        [BsonId]
        public ObjectId Id { get; set; }

        // Un identificador semántico, p.ej. "correction" o "structuring"
        public string Key { get; set; } = null!;

        // Todo el contenido del prompt
        public string Text { get; set; } = null!;
    }
}
