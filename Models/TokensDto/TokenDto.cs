using ChatAPI.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAPI.Models.TokensDto
{
    public class TokenDto
    {
        public int Id { get; set; }
        [Required]
        public Guid TokenNumber { get; set; }
        [Required]
        public TokenType Type { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }

        public string Nickname { get; set; }
    }
}
