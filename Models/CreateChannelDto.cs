using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models
{
    public class CreateChannelDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public bool hasPassword { get; set; }
        [MaxLength(25)]
        public string Password { get; set; }
        [Required]
        public bool Blocked { get; set; }
    }
}
