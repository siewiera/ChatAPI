using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.AccountDto
{
    public class ResetPasswordDto
    {
        //User
        [Required]
        public string Password { get; set; }
        public DateTime ModifiedAt { get; set; }

        //Token
        public bool Used { get; set; }
    }
}
