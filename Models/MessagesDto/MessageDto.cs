using ChatAPI.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAPI.Models.MessagesDto
{
    public class MessageDto
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
        //channel
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //user
        public string Nickname { get; set; }
    }
}
