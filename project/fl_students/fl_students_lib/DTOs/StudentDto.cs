using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fl_students_lib.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CI { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Career { get; set; }
        public string Semester { get; set; }
    }
}
