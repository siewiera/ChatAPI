using ChatAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.SessionsDto
{
    public class SessionDto
    {
        public int Id { get; set; }
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        public DateTime LastAction { get; set; }
        public string Ip { get; set; }

        //user
        public int UserId { get; set; }
        public string Nickname { get; set; }
    }
}
