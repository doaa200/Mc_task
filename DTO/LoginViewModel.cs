using System.ComponentModel.DataAnnotations;

namespace Mc_Task.DTO
{
    public class LoginViewModel
    {

        [Required]
        [StringLength(60)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
