using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fl_students_lib.DTOs
{
    public class RegisterStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CI { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int CareerId { get; set; }
    }
}
