namespace ufl_id.DTO
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CI { get; set; } // Cédula de identidad o documento equivalente
        public DateOnly Birthdate { get; set; }
        public string PhoneNumber { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

}
