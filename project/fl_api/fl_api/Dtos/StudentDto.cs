using System.Text.Json.Serialization;

namespace fl_api.Dtos
{
    public class StudentDto
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = null!;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = null!;
    }
}
