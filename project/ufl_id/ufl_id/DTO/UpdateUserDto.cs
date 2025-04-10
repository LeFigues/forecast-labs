namespace ufl_id.DTO
{
    public class UpdateUserDto
    {
        public string Email { get; set; }  // Nuevo email del usuario
        public int Status { get; set; }    // Nuevo estado (activo, inactivo, etc.)
    }
}
