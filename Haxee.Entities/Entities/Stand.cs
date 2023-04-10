namespace Haxee.Entities.DTOs
{
    public class Stand : AbstractEntity
    {
        public string? SupervisorId { get; set; }
        public virtual User? Supervisor { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required int Number { get; set; }
        public required TimeSpan Penalty { get; set; }
        public virtual List<StandVisit> StandVisits { get; set; } = new();
    }
}
