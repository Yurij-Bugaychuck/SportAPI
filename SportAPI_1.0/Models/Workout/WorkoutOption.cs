using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models
{
    public class WorkoutOption
    {
        [Key]
        public Guid WorkoutOptionId { get; set; }
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
       


        public string Key { get; set; }
        public int Value { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    }
}
