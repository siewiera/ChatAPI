using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAPI.Entities
{
    public class Conversation
    {
        public int Id { get; set; }
        public DateTime CreatAt { get; set; }

        public int ChannelId { get; set; }
        public virtual Channel Channel { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
