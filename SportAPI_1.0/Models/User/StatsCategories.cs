﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models.User
{
    public class StatsCategory
    {
        [Key]
        public Guid StatsCategoryId { get; set; }
        
        public string Name { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public List<UserStat> UsersStatsInCategory { get; set; }
    }
}
