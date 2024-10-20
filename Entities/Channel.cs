using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Entities
{
    public class Channel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public bool hasPassword { get; set; }
        public string Password { get; set; }
        [Required]
        public bool Blocked { get; set; }

        public virtual ICollection<Conversation> Conversations { get; set; }
    }
}
