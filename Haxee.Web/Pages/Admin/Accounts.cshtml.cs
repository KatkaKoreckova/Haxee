using Haxee.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Haxee.Web.Pages.Admin
{
    [Authorize(Policy = Constants.Policies.RequireAdmin)]
    public class AccountsModel : PageModel
    {
        private readonly DataContext _dataContext;
        public List<User> Users = new();

        public AccountsModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task OnGetAsync()
        {
            Users = await _dataContext.Users.ToListAsync();
        }
    }
}
