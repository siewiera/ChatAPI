using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models
{
    public class UpdateUserDto
    {
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
    }
}
