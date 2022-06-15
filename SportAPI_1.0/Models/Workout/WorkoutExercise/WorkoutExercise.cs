using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models.Workout.WorkoutExercise
{
    public class WorkoutExercise
    {
        [Key]
        public Guid? WorkoutExerciseId { get; set; } = Guid.NewGuid();

        [ForeignKey("Workout")]
        public Guid? WorkoutId { get; set; }

        public bool IsSet { get; set; } = false;

        public int? Order { get; set; } = 0;

        public int? Repeats { get; set; } = 0;
        public int? Calories { get; set; } = 0;

        public int? Sets { get; set; } = 0;

        public int? Duration { get; set; } = 0;
        public int? Weight { get; set; }

        public string Name { get; set; }
        public string About { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public Guid? WorkoutExerciseCategoryId { get; set; }
        public WorkoutExerciseCategory WorkoutExerciseCategory { get; set; }
    }
}