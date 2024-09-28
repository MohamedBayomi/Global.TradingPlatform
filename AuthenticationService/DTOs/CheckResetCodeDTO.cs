using System.ComponentModel.DataAnnotations;

namespace Authentication_Service.DTOs
{
    public class CheckResetCodeDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ResetCode { get; set; }
    }
}
