using BoardEx.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public Login LoginViewModel { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string? ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await signInManager.PasswordSignInAsync(
                LoginViewModel.Username, LoginViewModel.Password, false, false);

                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return RedirectToPage(ReturnUrl);
                    }

                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Notification"] = new Notification
                    {
                        Type = Enums.NotificationType.Error,
                        Message = "Nepavyko prisijungti!"
                    };

                    return Page();
                }
            }
            else
            {
                return Page();
            }
            
        }
    }
}
