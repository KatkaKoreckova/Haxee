namespace Haxee.Entities.Models
{
    /// <summary>
    /// Model pre zobrazenie dát o počte súťažiacich na štarte, na trati a v cieli.
    /// </summary>
    public class AttendeesStatsModel
    {
        public int Waiting { get; set; }
        public int InProgress { get; set; }
        public int Completed { get; set; }
    }
}
