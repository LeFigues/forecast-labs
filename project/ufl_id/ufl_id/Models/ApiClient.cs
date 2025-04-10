using System.ComponentModel.DataAnnotations;

namespace ufl_id.Models
{
    public class ApiClient
    {
        [Key]
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Name { get; set; }
    }
}
