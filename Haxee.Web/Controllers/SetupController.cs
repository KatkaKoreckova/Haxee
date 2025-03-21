using Haxee.Entities.Entities.Mqtt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Haxee.Web.Controllers;

/// <summary>
/// API na ukladanie nastavení MQTT Consumer-a
/// </summary>
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class SetupController : ControllerBase
{
    private readonly IDbContextFactory<DataContext> _dbContextFactory;

    public SetupController(IDbContextFactory<DataContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// API endpoint na uloženie konfigurácie.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        using var db = _dbContextFactory.CreateDbContext();
        var targetYear = await db.Years.AsNoTracking().SingleOrDefaultAsync(x => x.YearValue.Equals(DateTime.Now.Year));

        if (targetYear is null || targetYear.BrokerIp is null)
            return NotFound();

        return Ok(targetYear);
    }

    /// <summary>
    /// API endpoint na získanie konfigurácie.
    /// </summary>
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

    /// <summary>
    /// API endpoint na zmazanie konfigurácie.
    /// </summary>
    [HttpDelete("{currentYear}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int currentYear)
    {
        using var db = _dbContextFactory.CreateDbContext();

        var targetYear = await db.Years.SingleOrDefaultAsync(x => x.YearValue.Equals(currentYear));

        Console.WriteLine("get target year");

        if (targetYear is null)
            return NotFound();

        Console.WriteLine("target year not null");
        db.Years.Remove(targetYear);
        await db.SaveChangesAsync();

        Console.WriteLine("execute delete");

        return Ok();
    }
}