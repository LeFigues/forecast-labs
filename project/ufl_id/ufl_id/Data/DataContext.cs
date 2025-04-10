using Microsoft.EntityFrameworkCore;
using System.Data;
using ufl_id.Models;

namespace ufl_id.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // DbSet para cada entidad
        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<ApiToken> ApiTokens { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<ApiClient> ApiClients { get; set; }
        public DbSet<ApiScope> ApiScopes { get; set; }
        public DbSet<AuthorizationCode> AuthorizationCodes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales, por ejemplo:
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Person>()
                .HasIndex(p => p.CI)
                .IsUnique();

            modelBuilder.Entity<ApiClient>()
                .HasIndex(c => c.ClientId)
                .IsUnique();

            modelBuilder.Entity<ApiScope>()
                .HasIndex(s => s.ScopeName)
                .IsUnique();

            // Relación uno a uno entre User y Person (opcional si es necesario establecer explícitamente)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PersonId);
            modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);
        }
    }

}
