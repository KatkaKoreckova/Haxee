namespace Haxee.Entities.DTOs.Hifi
{
    public class HifiLeaderboardPerson : AbstractEntity
    {
        public required string Name { get; set; }
        public int SkipedStands { get; set; }
        public int NotFinishedStands { get; set; }
        public int FinishedStands { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan Penalty { get; set; }
    }
}
