﻿using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel.Identity
{
    public class SignUpViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage ="Email Is Required")]
        [EmailAddress(ErrorMessage = "Email Is Invalid")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Display(Name = "ConfirmPassword")]
        [Compare("Password",ErrorMessage ="Confirm Password Not Match With Password !!")]
        public string ConfirmPassword { get; set; } = null!;

        [Display(Name ="Is Agree")]
        public bool IsAgree { get; set; }
    }
}
