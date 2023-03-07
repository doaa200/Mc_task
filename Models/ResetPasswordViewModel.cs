using System.ComponentModel.DataAnnotations;

namespace Mc_Task.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Token { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; }=null!;

        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string NewPassword { get; set; }=null !;

        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
