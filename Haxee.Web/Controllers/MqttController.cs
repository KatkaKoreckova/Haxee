using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> PostAsync(
        [FromBody] AttendeeInformationDTO attendeeInformation
    )
    {
        using var db = _dbContextFactory.CreateDbContext();

        var targetAttendee = await db
            .Attendees.Include(x => x.Activity)
            .ThenInclude(x => x.Stands)
            .ThenInclude(x => x.StandVisits)
            .Include(x => x.Activity)
            .ThenInclude(x => x.Stands)
            .ThenInclude(x => x.ScheduledVisits)
            .Include(x => x.StandVisits)
            .Include(x => x.ScheduledVisit)
            .SingleOrDefaultAsync(x => attendeeInformation.CardId.Equals(x.CardId));

        if (targetAttendee is null)
            return NotFound();

        if (targetAttendee.Activity.Status != ActivityStatus.InProgress)
            return StatusCode(403);

        var currentStand = await db
            .Stands.Where(x => x.ActivityId.Equals(targetAttendee.ActivityId))
            .Include(x => x.Device)
            .Where(x => x.Device != null && x.Device.Identifier.Equals(attendeeInformation.Trigger))
            .FirstOrDefaultAsync();

        if (currentStand is null)
            return NotFound();

        if (currentStand.IsQuiz)
            return Conflict();

        if (currentStand.IsStartingStand)
        {
            if (targetAttendee.StartedAt is null)
            {
                targetAttendee.StartedAt = DateTime.Now;
            }

            if (
                targetAttendee.EndedAt is not null
                || targetAttendee.StandVisits.Any(x => x.LeaveTime == null)
            )
                return StatusCode(403);

            var remainingStands = targetAttendee
                .Activity.Stands.Where(x => !x.IsStartingStand)
                .Where(x => !targetAttendee.StandVisits.Any(y => y.StandId.Equals(x.Id)));

            if (remainingStands.Any())
            {
                Stand? targetStand = remainingStands
                    .Where(x => x.RemainingCapacity > 0 && !x.IsQuiz)
                    .OrderByDescending(x => x.RemainingCapacity)
                    .FirstOrDefault();

                if (targetStand is null)
                    targetStand = remainingStands
                        .Where(x => x.AlmostEndingVisits > 0 && !x.IsQuiz)
                        .OrderByDescending(x => x.AlmostEndingVisits)
                        .FirstOrDefault();

                if (targetStand is null)
                    targetStand = remainingStands
                        .Where(x => x.IsQuiz && x.RemainingCapacity > 0)
                        .OrderByDescending(x => x.RemainingCapacity)
                        .FirstOrDefault();

                if (targetStand is null)
                    targetStand = remainingStands
                        .Where(x => x.IsQuiz && x.AlmostEndingVisits > 0)
                        .OrderByDescending(x => x.AlmostEndingVisits)
                        .FirstOrDefault();

                if (targetStand is null)
                    targetStand = remainingStands
                        .OrderByDescending(x => x.RemainingCapacity)
                        .ThenByDescending(x => x.AlmostEndingVisits)
                        .First();

                targetStand.ScheduledVisits.Add(
                    new ScheduledVisit()
                    {
                        AttendeeId = targetAttendee.Id,
                        StandId = targetStand.Id
                    }
                );
            }
            else
                targetAttendee.EndedAt = DateTime.Now;
        }
        else if (targetAttendee.StartedAt == null)
            return StatusCode(403);
        else
        {
            var existingStandVisit = targetAttendee.StandVisits.SingleOrDefault(x =>
                x.StandId.Equals(currentStand.Id)
            );

            if (existingStandVisit is not null)
                existingStandVisit.LeaveTime = DateTime.Now;
            else
            {
                if (targetAttendee.ScheduledVisit is not null)
                {
                    if (currentStand.Id != targetAttendee.ScheduledVisit.StandId)
                        return StatusCode(403);

                    db.Remove(targetAttendee.ScheduledVisit);
                }

                targetAttendee.StandVisits.Add(
                    new StandVisit
                    {
                        StartTime = DateTime.Now,
                        StandId = currentStand.Id,
                        AttendeeId = targetAttendee.Id
                    }
                );
            }
        }

        await db.SaveChangesAsync();
        return Ok();
    }
}
