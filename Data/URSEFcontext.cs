using Microsoft.EntityFrameworkCore;
using UserRolesStatusEFCore.Models;

namespace UserRolesStatusEFCore.Data
{
    public class URSEFcontext : DbContext
    {
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRoles> UserRoles { get; set; } = null!;
        public DbSet<Status> Status { get; set; } = null!;
        public DbSet<UserStatus> UserStatus { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserRoles>()
            //      .HasKey(m => new { m.UserId, m.RoleId });

            //modelBuilder.Entity<UserStatus>()
            //      .HasKey(m => new { m.UserId, m.StatusId });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=URSEFdatabase;Integrated Security=True;TrustServerCertificate=True"); 
        }

    }
}
