using Haxee.Entities.Entities.Mqtt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Haxee.Web.Controllers;

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class SetupController : ControllerBase
{
    private readonly IDbContextFactory<DataContext> _dbContextFactory;

    public SetupController(IDbContextFactory<DataContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        using var db = _dbContextFactory.CreateDbContext();
        var targetYear = await db.Years.AsNoTracking().SingleOrDefaultAsync(x => x.YearValue.Equals(DateTime.Now.Year));

        if (targetYear is null || targetYear.BrokerIp is null)
            return NotFound();

        return Ok(targetYear);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CurrentYear currentYear)
    {
        using var db = _dbContextFactory.CreateDbContext();

        var targetYear = await db.Years.SingleOrDefaultAsync(x => x.YearValue.Equals(currentYear.Year));

        if (targetYear is null)
            return NotFound();

        targetYear.BrokerIp = currentYear.BrokerIP;
        targetYear.BrokerPort = currentYear.BrokerPort;
        targetYear.GlobalTopic = currentYear.GlobalTopic;

        await db.SaveChangesAsync();
        return Ok();
    }
}