namespace Haxee.Entities.Models
{
    public class AttendeeLeaderboardModel
    {
        public string FullName { get; set; } = string.Empty;

        public TimeSpan? Time { get; set; }

        public int VisitedStands { get; set; }
    }
}
