namespace Haxee.Entities.Models
{
    /// <summary>
    /// Model pre zobrazenie dát o súťažiacom do výsledkovej listiny.
    /// </summary>
    public class AttendeeLeaderboardModel
    {
        public string FullName { get; set; } = string.Empty;

        public TimeSpan? Time { get; set; }

        public int VisitedStands { get; set; }
    }
}
