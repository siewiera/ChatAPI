using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.ChannelsDto
{
    public class ChannelDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public bool hasPassword { get; set; }
        [Required]
        public bool Blocked { get; set; }
    }
}
