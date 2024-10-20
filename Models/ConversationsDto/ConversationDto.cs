using ChatAPI.Entities;
using ChatAPI.Models.MessagesDto;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAPI.Models.ConversationsDto
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public DateTime CreatAt { get; set; }

        public int ChannelId { get; set; }
        public string ChannelName { get; set; }

        public List<MessageDto> Messages { get; set; }
    }
}
