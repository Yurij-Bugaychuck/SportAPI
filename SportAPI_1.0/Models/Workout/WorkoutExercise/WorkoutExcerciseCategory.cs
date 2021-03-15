using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models
{

    public class WorkoutExcerciseCategory
    {
        [Key]
        public Guid WorkoutExcerciseEategoryId { get; set; }
        public Guid WorkoutExcerciseId { get; set; }
        [ForeignKey("WorkoutExcerciseId")]
        public WorkoutExcercise WorkoutExcercise { get; set; }

        public Guid WorkoutExcerciseCategoriesId { get; set; }
        [ForeignKey("WorkoutExcerciseCategoriesId")]
        public WorkoutExcerciseCategories WorkoutExcerciseCategories { get; set; }


        public string Key { get; set; }
        public int Value { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    }
}
