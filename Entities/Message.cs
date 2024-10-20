using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAPI.Entities
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string Contents { get; set; }

        public int ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
