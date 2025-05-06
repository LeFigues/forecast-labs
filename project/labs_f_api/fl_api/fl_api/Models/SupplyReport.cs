using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models
{
    public class SupplyReport
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public int QuantityUsed { get; set; }
        public int QuantityDamaged { get; set; }
        public int QuantityConsumed { get; set; }
        public int QuantityRequired { get; set; }
    }

}
