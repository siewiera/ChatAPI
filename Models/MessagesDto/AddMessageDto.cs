using ChatAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.MessagesDto
{
    public class AddMessageDto
    {
        //message
        public int Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string Contents { get; set; }
        //public int ConversationId { get; set; }

        //conversation new
        public DateTime CreatAt { get; set; }
        public int ChannelId { get; set; }

        //user
        public int UserId { get; set; }
    }
}
