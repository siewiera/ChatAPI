using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace ChatAPI.Entities
{
    public class Session
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        public DateTime LastAction { get; set; }
        public string Ip { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
