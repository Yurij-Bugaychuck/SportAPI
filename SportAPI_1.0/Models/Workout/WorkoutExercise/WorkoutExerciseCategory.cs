using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models.Workout.WorkoutExercise
{
    public class WorkoutExerciseCategory
    {
        [Key]
        public Guid? WorkoutExerciseCategoryId { get; set; }

        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<WorkoutExercise> WorkoutExercises { get; set; } = new();
    }
}