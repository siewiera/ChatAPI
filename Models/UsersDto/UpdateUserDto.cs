using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAPI.Models.UsersDto
{
    public class UpdateUserDto
    {
        public string Nickname { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
