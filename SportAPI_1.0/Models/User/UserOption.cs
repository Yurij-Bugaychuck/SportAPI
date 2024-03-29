﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAPI.Models.User
{
    public class UserOption
    {
        [Key]
        public Guid UserOptionsId { get; set; } = Guid.NewGuid();
        
        [ForeignKey("User")]
        public Guid? UserId { get; set; }

        public string Key { get; set; }
        
        public string Value { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}