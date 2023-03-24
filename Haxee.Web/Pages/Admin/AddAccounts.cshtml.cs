using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Haxee.Web.Pages.Admin
{
    [Authorize(Policy = Constants.Policies.RequireAdmin)]
    public class AddAccountsModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public InputModel Input { get; set; } = new();


        public AddAccountsModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var users = JsonSerializer.Deserialize<List<AddAccountModel>>(Input.Json);

                if (users is not null)
                {
                    var failedAccounts = new List<string>();

                    foreach (var user in users)
                    {
                        var result = await _userManager.CreateAsync(new User
                        {
                            UserType = Entities.Enums.UserType.Kid,
                            Name = user.Name,
                            DateOfBirth = DateTime.Parse(user.DateOfBirth),
                            SuperInstructor = user.SuperInstructor,
                            Email = user.Email,
                            UserName = user.Email
                        });

                        if (result.Errors.Any())
                            failedAccounts.Add(user.Email);
                    }

                    if (!failedAccounts.Any())
                        return LocalRedirect("~/admin/accounts");

                    ModelState.AddModelError("", $"Nevytvorene ucty {string.Join(", ", failedAccounts)}");
                    return Page();
                }

                ModelState.AddModelError("", "Chyba pri nahravani");
            }

            return Page();
        }
    }

    public class InputModel
    {
        [Required]
        [Display(Prompt = "[\n\t{\n\t\t\"Name\":\"Xx\",\n\t\t\"Email\":\"x@x.com\",\n\t\t\"DateOfBirth\":\"12.1.2002\"\n\t}\n]")]
        [DataType(DataType.MultilineText)]
        public string Json { get; set; } = string.Empty;

    }
}
