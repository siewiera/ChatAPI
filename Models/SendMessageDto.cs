using ChatAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models
{
    public class SendMessageDto
    {
        [Required]
        public int ChannelId { get; set; }

        public List<MessageDto> Messages { get; set; }
        public List<UserConversationDto> UserConversations { get; set; }
    }
}
