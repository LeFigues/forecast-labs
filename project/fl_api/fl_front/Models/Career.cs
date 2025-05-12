using System.Text.Json.Serialization;

namespace fl_front.Models
{
    public class Career
    {
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("subjects")]
        public SubjectListWrapper? SubjectsWrapper { get; set; }

        [JsonIgnore]
        public List<Subject> Subjects => SubjectsWrapper?.Values ?? new();
    }
}
