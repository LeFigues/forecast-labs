namespace fl_front.Models
{
    public class MaterialList
    {
        public List<MaterialItem>? Equipment { get; set; } = new();
        public List<MaterialItem>? Supplies { get; set; } = new();
        public List<MaterialItem>? Reactives { get; set; } = new();
    }
}
