using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel.Identity
{
    public class LogInViewModel
    {
        [EmailAddress]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember Me")]
        public string RememberMe { get; set; }
    }
}
