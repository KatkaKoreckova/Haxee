using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Haxee.Web.Controllers;

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class PasswordController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly MailService _mailService;

    public PasswordController(UserManager<User> userManager, MailService mailService)
    {
        _userManager = userManager;
        _mailService = mailService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromQuery] string id)
    {
        var targetUser = await _userManager.FindByIdAsync(id);

        if (targetUser is null)
            return NotFound();

        var newPassword = PasswordHelper.GeneratePassword();

        await _userManager.ResetPasswordAsync(targetUser, await _userManager.GeneratePasswordResetTokenAsync(targetUser), newPassword);
        await _mailService.SendNewPasswordAsync(targetUser.Email!, newPassword);

        return Ok();
    }
}