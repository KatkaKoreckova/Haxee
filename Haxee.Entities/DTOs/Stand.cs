namespace Haxee.Entities.DTOs
{
    public class Stand : AbstractEntity
    {
        public virtual List<User> CurrentPeople { get; set; } = new();

        public int? SupervisorId { get; set; }
        public virtual User? Supervisor { get; set; }
    }
}
