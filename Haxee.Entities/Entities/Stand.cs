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
        public required int YearId { get; set; }
        public bool IsQuiz { get; set; }
        public List<string> QuestionsAndAnswers { get; set; } = new();
        public virtual Year Year { get; set; } = null!;
        public virtual List<StandVisit> StandVisits { get; set; } = new();
    }
}
