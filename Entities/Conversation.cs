using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Entities
{
    public class Conversation
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public virtual Channel Channel { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserConversation> UserConversations { get; set; }
    }
}
