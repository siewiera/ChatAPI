using ChatAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.ChannelsDto
{
    public class UpdateChannelDto
    {
        //[Required]
        [MaxLength(50)]
        public string Name { get; set; } = "";
        [Required]
        public bool hasPassword { get; set; }
        public string Password { get; set; }
    }
}
