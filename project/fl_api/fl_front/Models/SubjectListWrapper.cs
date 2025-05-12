using System.Text.Json.Serialization;

namespace fl_front.Models
{
    public class SubjectListWrapper
    {
        [JsonPropertyName("$values")]
        public List<Subject> Values { get; set; } = new();
    }
}
