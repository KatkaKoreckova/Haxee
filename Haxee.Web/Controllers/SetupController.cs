using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// API na načítanie nastavení MQTT Consumer-a
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
    /// API endpoint na načítanie konfigurácie.
    /// </summary>
    [HttpGet("{activityId}")]
    public async Task<IActionResult> GetAsync([FromRoute] int activityId)
    {
        using var db = _dbContextFactory.CreateDbContext();

        var targetActivity = await db
            .Activities.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(activityId));

        if (targetActivity is null)
            return NotFound();

        return Ok(targetActivity);
    }
}
