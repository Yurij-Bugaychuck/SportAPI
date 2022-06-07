using System;
using System.Collections.Generic;

namespace SportAPI.Models.OutModel
{
    public class UserStats
    {
        public Guid? UserStatsId { get; set; } = Guid.NewGuid();

        public Guid? UserId { get; set; }

        public string Key { get; set; }
        
        public List<UserStatValue> Values { get; set; }

        public Guid? StatsCategoryId { get; set; }
    }
}