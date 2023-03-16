using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Haxee.Web.Pages.Auth
{
    [Authorize(Policy = Constants.Policies.RequireAdmin)]
    public class AdminModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
