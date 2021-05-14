using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models
{
    public class WorkoutExcercise
    {
        [Key]
        public Guid WorkoutExcerciseId { get; set; } = Guid.NewGuid();
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]

        public bool IsSet { get; set; } = false;

        public uint Order { get; set; } = 0;

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


        public List<WorkoutExcerciseCategory> Categories { get; set; }
        public List<WorkoutExcerciseOption> Options { get; set; } = new List<WorkoutExcerciseOption>();



    }
}
