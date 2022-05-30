using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SportAPI.Models.User;

namespace SportAPI.Models
{
    public sealed class SportContext : DbContext
    {
        public DbSet<User.User> Users { get; set; }
        public DbSet<UserStat> UsersStats { get; set; }
        public DbSet<UserOption> UsersOptions { get; set; }
        public DbSet<StatsCategory> StatsCategories { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutOption> WorkoutsOptions { get; set; }
        public DbSet<WorkoutExcercise> WorkoutsExcercises { get; set; }
        public DbSet<WorkoutExcerciseCategory> WorkoutsExcercisesCategory { get; set; }
        public DbSet<WorkoutExcerciseCategories> WorkoutsExcercisesCategories { get; set; }
        public DbSet<WorkoutExcerciseOption> WorkoutsExcercisesOptions { get; set; }
        
        public IConfiguration Configuration { get; }
        
        public SportContext(DbContextOptions<SportContext> options, IConfiguration configuration) 
            : base(options)
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            
            //this.ChangeTracker.LazyLoadingEnabled = false;

            this.Configuration = configuration;
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

            modelBuilder.Entity<WorkoutExcercise>().HasMany<WorkoutExcerciseOption>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}