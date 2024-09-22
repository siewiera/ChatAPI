using ChatAPI.Entities;

namespace ChatAPI.Models
{
    public class ConversationDto
    {
        public int Id { get; set; }
        //public string ChannelName { get; set; }
        public int ChannelDtoId { get; set; }
        //public virtual ChannelDto ChannelDto { get; set; }

        public List<MessageDto> Messages { get; set; }

        public List<UserConversationDto> UserConversations { get; set; }
    }
}
