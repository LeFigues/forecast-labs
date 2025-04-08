namespace fl_api.DTOs
{
    public class MaterialsDto
    {
        public List<MaterialItemDto> Equipment { get; set; } = new();
        public List<MaterialItemDto> Supplies { get; set; } = new();
    }
}
