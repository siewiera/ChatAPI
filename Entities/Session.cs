﻿using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace ChatAPI.Entities
{
    public class Session
    {       
        public int Id { get; set; }
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        public DateTime LastAction { get; set; }
        public string Ip { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
