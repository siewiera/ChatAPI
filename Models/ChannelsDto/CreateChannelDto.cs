using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.ChannelsDto
{
    public class CreateChannelDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public bool hasPassword { get; set; }
        public string Password { get; set; }
        [Required]
        public bool Blocked { get; set; }
    }
}
