using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.AccountDto
{
    public class ChangeEmailDto
    {
        //User
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime ModifiedAt { get; set; }

        //Token
        public bool Used { get; set; }
    }
}
