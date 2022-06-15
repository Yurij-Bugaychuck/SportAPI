using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models.Workout
{ 
    public class Workout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WorkoutId { get; set; }
        
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        
        [ForeignKey("UserId")]
        User.User User { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string About { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? StartFrom { get; set; }
        
        public bool? IsPublished { get; set; } = false;
    }
}
