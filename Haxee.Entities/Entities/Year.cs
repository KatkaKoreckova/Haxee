namespace Haxee.Entities.Entities
{
    public class Year : AbstractEntity
    {
        public required int YearValue { get; set; }
        public string? BrokerIp { get; set; }
        public int? BrokerPort { get; set; }
        public string? GlobalTopic { get; set; }
        public virtual List<Stand> Stands { get; set; } = new();
        public virtual List<Attendee> Attendees { get; set; } = new();
        public YearStatus Status { get; set; } = YearStatus.Pending;
    }
}
