using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Haxee.Web.Controllers;

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class MqttController : ControllerBase
{
    private readonly IDbContextFactory<DataContext> _dbContextFactory;

    public MqttController(IDbContextFactory<DataContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AttendeeInformationDTO attendeeInformation)
    {
        using var db = _dbContextFactory.CreateDbContext();

        var targetAttendee = await db.Attendees
            .Include(x => x.Year)
                .ThenInclude(x => x.Stands)
            .Include(x => x.StandVisits)
            .SingleOrDefaultAsync(x => attendeeInformation.CardId.Equals(x.CardId));

        if (targetAttendee is null)
            return NotFound();

        if (targetAttendee.Year.Status != YearStatus.InProgress)
            return StatusCode(403);

        if (attendeeInformation.Trigger.Contains("start"))
        {
            if (targetAttendee.StartedAt is null)
            {
                targetAttendee.StartedAt = DateTime.Now;
            }
            else if (targetAttendee.EndedAt is null)
            {
                if (!targetAttendee.Year.Stands.Count.Equals(targetAttendee.StandVisits.Count) || targetAttendee.StandVisits.Any(x => x.LeaveTime == null))
                    return StatusCode(403);

                targetAttendee.EndedAt = DateTime.Now;
            }
            else
                return BadRequest();
        } else
        {
            if (!attendeeInformation.Trigger.Contains('S'))
                return BadRequest();

            if (targetAttendee.StartedAt is null)
                return StatusCode(403);

            var targetStand = await db.Stands.Where(x => x.YearId.Equals(targetAttendee.YearId)).SingleOrDefaultAsync(x => $"S{x.Number}".Equals(attendeeInformation.Trigger));
            
            if (targetStand is null)
                return NotFound();

            if (targetStand.IsQuiz)
                return Conflict();

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