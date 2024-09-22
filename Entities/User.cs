using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(25)] 
        public string Username { get; set; }
        public string Nickname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(25)]
        public string Password { get; set; }
        [Required]
        public bool Blocked { get; set; }

        public virtual ICollection<UserConversation> UserConversations { get; set; }
    }
}
