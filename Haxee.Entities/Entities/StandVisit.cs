namespace Haxee.Entities.Entities
{
    public class StandVisit : AbstractEntity
    {
        public required int StandId { get; set; }
        public virtual Stand Stand { get; set; } = null!;

        public required int AttendeeId { get; set; }
        public virtual Attendee Attendee { get; set; } = null!;

        public required StandVisitStatus Status { get; set; }

        public TimeSpan Penalty { get; set; }

        public bool Skipped { get; set; }

        public required DateTime ArrivalTime { get; set; }

        public DateTime? EndWaitTime { get; set; }

        public DateTime? LeaveTime { get; set; }
    }
}
