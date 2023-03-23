using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Haxee.Web.Pages.Admin
{
    [Authorize(Policy = Constants.Policies.RequireAdmin)]
    public class MenuModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
