using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Email Is Invalid")]
        public string Email { get; set; } = null!;


    }
}
