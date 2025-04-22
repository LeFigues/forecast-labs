using System.ComponentModel.DataAnnotations;

namespace fl_students_lib.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public string Day { get; set; }
        public TimeOnly StartAt { get; set; }
        public TimeOnly EndAt { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public int GroupId { get; set; }
        public SGroup? Group { get; set; }
    }
}
