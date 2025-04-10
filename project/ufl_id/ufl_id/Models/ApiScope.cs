using System.ComponentModel.DataAnnotations;

namespace ufl_id.Models
{
    public class ApiScope
    {
        [Key]
        public int Id { get; set; }
        public string ScopeName { get; set; }
        public string Description { get; set; }
    }
}
