using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TimetableHelper.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TimetableHelper.Pages
{
    [IgnoreAntiforgeryToken]
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public string LoginUsername { get; set; }

        [BindProperty]
        public string LoginPassword { get; set; }

        [BindProperty]
        public string NewUsername { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        [BindProperty]
        public string FormType { get; set; }

        public string? ErrorMessage { get; set; }

        public int UserCount { get; set; }

        public async Task OnGetAsync()
        {
            UserCount = await _userManager.Users.CountAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            UserCount = await _userManager.Users.CountAsync();

            if (FormType == "CreateUser" && UserCount == 0)
            {
                // Handle first user creation
                if (string.IsNullOrWhiteSpace(NewUsername) || string.IsNullOrWhiteSpace(NewPassword))
                {
                    ErrorMessage = "Zadejte platn� p�ihla�ovac� �daje.";
                    return Page();
                }

                var newUser = new User { UserName = NewUsername };
                var result = await _userManager.CreateAsync(newUser, NewPassword);

                if (result.Succeeded)
                {
                    ErrorMessage = "U�ivatel vytvo�en, nyn� se m��ete p�ihl�sit.";
                    return Page();
                }
                else
                {
                    ErrorMessage = "Chyba p�i vytv��en� nov�ho u�ivatele. Heslo mus� m�t alespo� 6 znak� a obsahovat ��slici.";
                    return Page();
                }
            }
            else if (FormType == "Login" && UserCount > 0)
            {
                // Handle login
                var result = await _signInManager.PasswordSignInAsync(LoginUsername, LoginPassword, false, false);
                if (result.Succeeded)
                {
                    return Redirect("~/");
                }
                else
                {
                    ErrorMessage = "Neplatn� p�ihla�ovac� �daje.";
                    return Page();
                }
            }
            return Page();
        }
    }
}
