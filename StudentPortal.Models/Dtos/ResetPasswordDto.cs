using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Models.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage ="Password mismatch")]
        public string ConfirmPassword { get; set; }
    }
}
