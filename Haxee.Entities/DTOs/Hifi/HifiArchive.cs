namespace Haxee.Entities.DTOs.Hifi
{
    public class HifiArchive : AbstractEntity
    {
        public required int Year { get; set; }
        public List<Stand> Stands { get; set; } = new();
        public List<HifiLeaderboardPerson> Leaderboard { get; set; } = new();
    }
}
