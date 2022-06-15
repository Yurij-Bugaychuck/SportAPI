using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models.Workout
{
    public class WorkoutOption
    {
        [Key]
        public Guid WorkoutOptionId { get; set; }
        
        [ForeignKey("Workout")] 
        public Guid WorkoutId { get; set; }
        public string Key { get; set; }
        public int Value { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
