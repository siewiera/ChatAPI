using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models
{
    public class MessageDto
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string Contents { get; set; }
    }
}
