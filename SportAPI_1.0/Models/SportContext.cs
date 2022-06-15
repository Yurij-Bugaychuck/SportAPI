using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SportAPI.Models.User;
using SportAPI.Models.Workout;
using SportAPI.Models.Workout.WorkoutExercise;

namespace SportAPI.Models
{
    public sealed class SportContext : DbContext
    {
        public DbSet<User.User> Users { get; set; }
        public DbSet<UserStat> UsersStats { get; set; }
        public DbSet<UserOption> UsersOptions { get; set; }
        public DbSet<StatsCategory> StatsCategories { get; set; }
        public DbSet<Workout.Workout> Workouts { get; set; }
        public DbSet<WorkoutOption> WorkoutsOptions { get; set; }
        public DbSet<WorkoutExercise> WorkoutsExercises { get; set; }
        public DbSet<WorkoutExerciseCategory> WorkoutsExerciseCategory { get; set; }
        public DbSet<WorkoutExerciseOption> WorkoutsExercisesOptions { get; set; }
        
        public IConfiguration Configuration { get; }
        
        public SportContext(DbContextOptions<SportContext> options, IConfiguration configuration) 
            : base(options)
        {
            //this.Database.EnsureDeleted();dot
            this.Configuration = configuration;
            // this.Database.EnsureCreated();
            
            //this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User.User>()
                .HasIndex(b => b.Email)
                .IsUnique();
            
            modelBuilder.Entity<User.User>()
                .HasIndex(b => b.Phone)
                .IsUnique(); 
           
            modelBuilder.Entity<User.User>()
                .HasIndex(b => b.Username)
                .IsUnique();

            modelBuilder.Entity<WorkoutExercise>().HasMany<WorkoutExerciseOption>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder
                .UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}