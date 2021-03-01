using Microsoft.EntityFrameworkCore;

namespace SportAPI.Models
{
    public class SportContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_stats> Users_stats { get; set; }
        public DbSet<User_options> Users_options { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(b => b.Email)
                .IsUnique();
            
            modelBuilder.Entity<User>()
                .HasIndex(b => b.Phone)
                .IsUnique(); 
            modelBuilder.Entity<User>()
                .HasIndex(b => b.Username)
                .IsUnique(); 
           
        }

        public SportContext(DbContextOptions<SportContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}