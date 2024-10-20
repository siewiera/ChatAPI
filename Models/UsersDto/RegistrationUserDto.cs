using ChatAPI.Enum;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.UsersDto
{
    public class RegistrationUserDto
    {
        //Token
        [Required]
        public Guid TokenNumber { get; set; }
        [Required]
        public TokenType Type { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public int UserId { get; set; }

        //User
        public string Nickname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public bool Blocked { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
