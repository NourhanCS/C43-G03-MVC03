using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="New Password Is Required")] 
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;
        [Display(Name = "ConfirmPassword")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password Not Match With Password !!")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; } = null!;

        
    }
}
