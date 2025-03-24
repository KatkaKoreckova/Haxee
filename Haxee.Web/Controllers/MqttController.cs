using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Haxee.Web.Controllers;

/// <summary>
/// API ovladač pre komunikáciu MQTT Consumer-a a databázi.
/// </summary>
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class MqttController : ControllerBase
{
    private readonly IDbContextFactory<DataContext> _dbContextFactory;

    public MqttController(IDbContextFactory<DataContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// API endpoint pre ukladanie dát z aplikácie MQTT Consumer do databázy
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AttendeeInformationDTO attendeeInformation)
    {
        using var db = _dbContextFactory.CreateDbContext();

        var targetAttendee = await db.Attendees
            .Include(x => x.Activity)
                .ThenInclude(x => x.Stands)
            .Include(x => x.StandVisits)
            .SingleOrDefaultAsync(x => attendeeInformation.CardId.Equals(x.CardId));

        if (targetAttendee is null)
            return NotFound();

        if (targetAttendee.Activity.Status != ActivityStatus.InProgress)
            return StatusCode(403);

        var targetStand = await db.Stands
            .Where(x => x.ActivityId.Equals(targetAttendee.ActivityId))
            .Include(x => x.Device)
            .Where(x => x.Device != null && x.Device.Identifier.Equals(attendeeInformation.Trigger))
            .FirstOrDefaultAsync();

        if (targetStand is null)
            return NotFound();

        if (targetStand.IsQuiz)
            return Conflict();

        if(targetStand.IsStartingStand)
        {
            if (targetAttendee.StartedAt is null)
            {
                targetAttendee.StartedAt = DateTime.Now;
            }
            else if (targetAttendee.EndedAt is null)
            {
                if (!targetAttendee.Activity.Stands
                    .Where(x => !x.IsStartingStand)
                    .Count()
                    .Equals(targetAttendee.StandVisits.Count) 
                    || 
                    targetAttendee.StandVisits.Any(x => x.LeaveTime == null))
                    return StatusCode(403);

                targetAttendee.EndedAt = DateTime.Now;
            }
            else
                return BadRequest();
        } else
        {
            var existingStandVisit = targetAttendee.StandVisits.SingleOrDefault(x => x.StandId.Equals(targetStand.Id));

            if(existingStandVisit is not null)
            {
                if (existingStandVisit.Status == StandVisitStatus.Waiting)
                {
                    existingStandVisit.Status = StandVisitStatus.Working;
                    existingStandVisit.EndWaitTime = DateTime.Now;
                }
                else if (existingStandVisit.Status == StandVisitStatus.Working)
                {
                    existingStandVisit.Status = StandVisitStatus.Done;
                    existingStandVisit.LeaveTime = DateTime.Now;
                }
                else
                    return StatusCode(403);
            } else
            {
                targetAttendee.StandVisits.Add(new StandVisit 
                {
                    ArrivalTime = DateTime.Now,
                    Status = StandVisitStatus.Waiting,
                    StandId = targetStand.Id,
                    AttendeeId = targetAttendee.Id
                });
            }
        }

        await db.SaveChangesAsync();
        return Ok();
    }
}