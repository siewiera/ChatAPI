using ChatAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool Blocked { get; set; }
    }
}
