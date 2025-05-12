using System.Text.Json.Serialization;

namespace fl_api.Dtos
{
    public class SubjectsResponseDto
    {
        [JsonPropertyName("$values")]
        public List<SubjectDto> Values { get; set; } = new();
    }
}
