using ChatAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.SessionsDto
{
    public class CreateSessionDto
    {
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        public DateTime LastAction { get; set; }
        public string Ip { get; set; }

        public int UserId { get; set; }
    }
}
