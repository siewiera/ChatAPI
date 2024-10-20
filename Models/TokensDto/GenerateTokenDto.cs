using ChatAPI.Entities;
using ChatAPI.Enum;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.TokensDto
{
    public class GenerateTokenDto
    {
        [Required]
        public Guid TokenNumber { get; set; }
        [Required]
        public TokenType Type { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }

        public int UserId { get; set; }
    }
}
