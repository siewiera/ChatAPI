using ChatAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models
{
    public class UpdateChannelDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public bool hasPassword { get; set; }
        [MinLength(5)]
        [MaxLength(25)]
        public string Password { get; set; }
        [Required]
        public bool Blocked { get; set; }
    }
}
