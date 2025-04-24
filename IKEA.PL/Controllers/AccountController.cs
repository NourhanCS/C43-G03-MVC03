using IKEA.DAL.Models;
using IKEA.DAL.Models.Identity;
using IKEA.PL.Helpers;
using IKEA.PL.ViewModel;
using IKEA.PL.ViewModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
     
        #region Services
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #endregion



        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var User = await userManager.FindByNameAsync(signUpViewModel.UserName);
            if(User is not null)
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This UserName is already in Use for other Account");
                return View(signUpViewModel);
            }

            User = new ApplicationUser()
            {
                FName = signUpViewModel.FirstName,
                LName = signUpViewModel.LastName,
                UserName = signUpViewModel.UserName,
                  Email = signUpViewModel.Email,
                  IsAgree = signUpViewModel.IsAgree ,
            };
       var Result = await userManager.CreateAsync(User,signUpViewModel.Password);

            if(Result.Succeeded)
                return RedirectToAction(nameof(LogIn));

            foreach (var error in Result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(signUpViewModel);

        }

        #endregion

        #region LogIn

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

                var User = await userManager.FindByEmailAsync(logInViewModel.Email);

            if (User is not null)
            {
                var result = await signInManager.PasswordSignInAsync(User, logInViewModel.Password, logInViewModel.RememberMe,lockoutOnFailure:true);

                if (result.IsNotAllowed)
                    ModelState.AddModelError(string.Empty, "Your Account Is Not Confirmed");

                if (result.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "Your Account Is Locked!");

                if (result.Succeeded)
                    return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ModelState.AddModelError(String.Empty, "InValid Login Attempt !");
            return View(logInViewModel);
        }



        #endregion


        #region SignOut
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
        #endregion

        #region Forget Password
        //Get
        public IActionResult ForgetPassword()
        {
            return View();

        }

        //Post
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid) //Server Side Validation
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var Token = await userManager.GeneratePasswordResetTokenAsync(user); //Token Valid For One Time 
                    var PasswordResetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = Token },Request.Scheme);
                    //https:\\localhost:7035\Account\ResetPassword\Nourhan21@gmail.com&token=dvhwgedwuegugygfcsjacjq12356lwdip2edofegwgd
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = user.Email,
                        Body = "ResetPassswordLink"
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email Is Not Valid");
            }
            return View(model);
        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        #endregion

    }
}
