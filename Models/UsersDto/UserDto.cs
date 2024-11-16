using ChatAPI.Entities;
using ChatAPI.Models.MessagesDto;
using ChatAPI.Models.TokensDto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAPI.Models.UsersDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        [Required]
        public bool Blocked { get; set; }
        [Required]
        public bool Active { get; set; }

        //public virtual Session Session { get; set; }
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        public DateTime LastAction { get; set; }

        public ICollection<MessageDto> Messages { get; set; }
        public ICollection<TokenDto> Tokens { get; set; }
    }
}
