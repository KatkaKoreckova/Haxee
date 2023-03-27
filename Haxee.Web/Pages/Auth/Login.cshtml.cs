using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Xml.Linq;
using System;

namespace Haxee.Web.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public InputModel Input { get; set; } = new();
        public string ReturnUrl { get; set; } = string.Empty;

        public LoginModel(SignInManager<User> signInManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            ReturnUrl = Url.Content("~/home");

            if (_signInManager.IsSignedIn(User))
                return LocalRedirect(ReturnUrl);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/home");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, true, lockoutOnFailure: false);

                if (result.Succeeded)
                    return LocalRedirect(ReturnUrl);

                ModelState.AddModelError(nameof(Input)+"."+nameof(Input.Password), "Zadaný účet neexistuje.");
            }

            return Page();
        }

        public class InputModel
        {
            [Required]
            [Display(Prompt = "jozef@lstme.sk")]
            public string Email { get; set; } = string.Empty;

            [Required]
            [Display(Prompt = "Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }
    }
}