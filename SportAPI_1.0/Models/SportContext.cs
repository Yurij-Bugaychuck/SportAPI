using Microsoft.EntityFrameworkCore;

namespace SportAPI.Models
{
    public class SportContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStat> UsersStats { get; set; }
        public DbSet<UserOption> UsersOptions { get; set; }
        public DbSet<StatsCategories> StatsCategories { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutOption> WorkoutsOptions { get; set; }
        public DbSet<WorkoutExcercise> WorkoutsExcercises { get; set; }
        public DbSet<WorkoutExcerciseCategory> WorkoutsExcercisesCategory { get; set; }
        public DbSet<WorkoutExcerciseCategories> WorkoutsExcercisesCategories { get; set; }
        public DbSet<WorkoutExcerciseOption> WorkoutsExcercisesOptions { get; set; }

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

            this.ChangeTracker.LazyLoadingEnabled = false;
        }
    }
}