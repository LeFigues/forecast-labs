using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fl_students_lib.Models
{
    public class SGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int SemesterId { get; set; }
        public Semester? Semester { get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int TeacherId { get; set; }
        public Person? Teacher { get; set; }

        public ICollection<GroupStudent>? Students { get; set; }
        public ICollection<Schedule>? Schedule { get; set; }
    }
}
