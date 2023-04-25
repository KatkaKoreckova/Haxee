namespace Haxee.Entities.DTOs
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public required UserType UserType { get; set; }

        public bool SuperInstructor { get; set; } = false;

        public string? CardId { get; set; }

        public virtual Stand? SupervisorOfStand { get; set; }

        public virtual List<Attendee> Attendances { get; set; } = new();
    }
}
