using System.ComponentModel.DataAnnotations;

namespace Authentication_Service.DTOs
{
    public class RegisterDTO
    {

        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(100)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        public string? UserName { get; set; }
        public int DepartmentId { get; set; }
        public int? ManagerId { get; set; }
    }
}
