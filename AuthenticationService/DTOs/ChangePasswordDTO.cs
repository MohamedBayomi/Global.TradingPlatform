using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Authentication_Service.DTOs
{
    public class ChangePasswordDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, PasswordPropertyText]
        public string NewPassword { get; set; }
    }
}
