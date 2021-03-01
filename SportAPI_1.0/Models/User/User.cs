using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models
{

   
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid User_id { get; set; }


        
        [StringLength(50)]
        public string Username { get; set; }

      
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string First_name { get; set; }
        [StringLength(50)]
        public string Last_name { get; set; }

       
        [StringLength(50)]
        public string Phone { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime Created_at { get; set; } = DateTime.UtcNow;


        List<User_stats> user_stats { get; set; }



    }
}
