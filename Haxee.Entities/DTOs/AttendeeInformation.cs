namespace Haxee.Entities.DTOs
{
    public class AttendeeInformation
    {
        public required string CardId { get; set; }

        public required DateTime DateTime { get; set; }
        public required Status Status { get; set; }
    }
}
