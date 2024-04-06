using Auth.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Server.Data
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext>options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
