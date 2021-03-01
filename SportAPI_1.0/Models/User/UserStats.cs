using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models
{

    public class User_stats
    {
        [Key]
        public Guid User_stats_id { get; set; }
        public Guid User_id { get; set; }
        [ForeignKey("User_id")]
        public User User { get; set; }


        public string key { get; set; }
        public int value { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime Created_at { get; set; } = DateTime.UtcNow;



    }
}
