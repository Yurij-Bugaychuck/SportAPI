using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models.User
{
    public class UserStat
    {
        [Key]
        public Guid UserStatsId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public string Key { get; set; }
        public int? Value { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("StatsCategory")]
        public Guid? StatsCategoryId { get; set; }
    }
}