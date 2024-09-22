using ChatAPI.Entities;

namespace ChatAPI.Models
{
    public class UserConversationDto
    {
        public int Id { get; set; }

        public int UserDtoId { get; set; }
        public virtual UserDto UserDto { get; set; }

        public int ConversationDtoId { get; set; }
        public virtual ConversationDto ConversationDto { get; set; }
    }
}
