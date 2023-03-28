namespace Haxee.Entities.DTOs
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public required UserType UserType { get; set; }

        public bool SuperInstructor { get; set; } = false;

        public int? CurrentStandId { get; set; }

        public virtual Stand? CurrentStand { get; set; }

        public virtual Stand? SupervisorOfStand { get; set; }

        public string CardId { get; set; } = string.Empty;
        public bool CurrentYear { get; set; } = false;
        public int SkipedStands { get; set; }
        public int NotFinishedStands { get; set; }
        public int FinishedStands { get; set; }
        public DateTime HifiStart { get; set; }
        public DateTime HifiEnd { get; set; }
        public TimeSpan Penalty { get; set; }
    }
}
