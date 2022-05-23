using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models
{
    public class WorkoutExcerciseCategories
    {
        [Key]
        public Guid Workout_excercise_categories_id { get; set; }

        public string name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime Created_at { get; set; } = DateTime.UtcNow;

        List<WorkoutExcerciseCategories> WorkoutExcercisesByCategory { get; set; }
    }
}