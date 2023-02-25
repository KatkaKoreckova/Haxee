namespace Haxee.Entities.DTOs
{
    public class AttendeeInformation
    {
        public required string CardId { get; set; }
        public required DateTime TimeStamp { get; set; }
        public required string Stand { get; set; }
        public required Status Status { get; set; }
    }
}
