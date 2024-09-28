using System.ComponentModel.DataAnnotations;

namespace Authentication_Service.DTOs
{
    public class ForgetPasswordDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
