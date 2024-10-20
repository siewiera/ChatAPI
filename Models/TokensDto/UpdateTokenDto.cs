using ChatAPI.Entities;
using ChatAPI.Enum;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.TokensDto
{
    public class UpdateTokenDto
    {
        public bool Used { get; set; }

        public int UserId { get; set; }
        //user
        [Required]
        public bool Active { get; set; }
    }
}
