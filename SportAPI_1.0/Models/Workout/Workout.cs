using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models
{

   
    public class Workout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WorkoutId { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        User User { get; set; }



        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string About { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsPublished { get; set; } = false;


        List<WorkoutOption> Options { get; set; }
        List<WorkoutExcercise> Excercises { get; set; }



    }
}
