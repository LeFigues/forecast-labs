using System.Text.Json.Serialization;

namespace fl_api.Dtos
{
    public class SubjectDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("careerId")]
        public int CareerId { get; set; }
    }
}
