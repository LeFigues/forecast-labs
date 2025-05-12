using System.Text.Json.Serialization;

namespace fl_api.Dtos
{
    public class CareersResponseDto
    {
        [JsonPropertyName("$values")]
        public List<CareerDto> Values { get; set; } = new();
    }
}
