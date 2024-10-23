using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ChatAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        [Required]
        public bool Blocked { get; set; }
        [Required]
        public bool Active { get; set; }

        public virtual Session Session { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
