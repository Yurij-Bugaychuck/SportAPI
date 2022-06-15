using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models.Workout.WorkoutExercise
{
    public class WorkoutExerciseOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WorkoutExerciseOptionId { get; set; }
        
        [ForeignKey("WorkoutExercise")]
        public Guid? WorkoutExerciseId { get; set; }

        [ForeignKey("WorkoutExerciseId")]
        public WorkoutExercise WorkoutExercise { get; set; }

        public string Key { get; set; }
        public int Value { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
