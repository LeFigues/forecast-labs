using System.Text.Json.Serialization;

namespace fl_api.Dtos
{
    public class CareerDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        // Aquí mapeamos el objeto completo de subjects
        [JsonPropertyName("subjects")]
        public SubjectsResponseDto SubjectsWrapper { get; set; } = new();

        // Propiedad de conveniencia que expone directamente la lista
        [JsonIgnore]
        public List<SubjectDto> Subjects => SubjectsWrapper.Values.ToList();
    }
}
