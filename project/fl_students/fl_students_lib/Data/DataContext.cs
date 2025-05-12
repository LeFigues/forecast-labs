using Microsoft.EntityFrameworkCore;
using fl_students_lib.Models;

namespace fl_students_lib.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // DbSet para cada modelo
        public DbSet<SGroup> SGroups { get; set; }
        public DbSet<GroupStudent> GroupStudents { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Faculty> Faculties { get; set; } // Agregado DbSet para Faculty

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones y restricciones adicionales
            modelBuilder.Entity<GroupStudent>()
                .HasOne(gs => gs.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(gs => gs.GroupId);

            modelBuilder.Entity<GroupStudent>()
                .HasOne(gs => gs.Student)
                .WithMany(s => s.Groups)
                .HasForeignKey(gs => gs.StudentId);

            // Relación uno a muchos entre Group y Schedule
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Schedule)
                .HasForeignKey(s => s.GroupId);

            // Relación uno a muchos entre Room y Schedule
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Schedules)
                .HasForeignKey(s => s.RoomId);

            // Relación uno a muchos entre Subject y Career
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Career)
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.CareerId);

            // Relación uno a muchos entre Group y Semester
            modelBuilder.Entity<SGroup>()
                .HasOne(g => g.Semester)
                .WithMany(s => s.Groups)
                .HasForeignKey(g => g.SemesterId);

            // Relación uno a muchos entre Group y Subject
            modelBuilder.Entity<SGroup>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Groups)
                .HasForeignKey(g => g.SubjectId);

            // Relación uno a muchos entre Group y Teacher (Person)
            modelBuilder.Entity<SGroup>()
                .HasOne(g => g.Teacher)
                .WithMany()
                .HasForeignKey(g => g.TeacherId);

            // Relación uno a muchos entre Faculty y Career
            modelBuilder.Entity<Career>()
                .HasOne(c => c.Faculty)
                .WithMany(f => f.Careers)
                .HasForeignKey(c => c.FacultyId);

            // Relación uno a muchos entre Faculty y Room
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Faculty)
                .WithMany(f => f.Rooms)
                .HasForeignKey(r => r.FacultyId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
