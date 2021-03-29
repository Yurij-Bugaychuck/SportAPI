﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models
{

    public class UserStat
    {
        [Key]
        public Guid UserStatsId { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }


        public string Key { get; set; }
        public int Value { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid StatsCategoryId { get; set; }
        [ForeignKey("StatsCategoryId")]
        public StatsCategories Category  { get; set; }



    }
}