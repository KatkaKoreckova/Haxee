namespace Haxee.Entities.Models
{
    /// <summary>
    /// Model pre zobrazenie dát o tom, kde je najdlhšie a kde najkratšie čakanie na stanovisku.
    /// </summary>
    public class StandStatsModel
    {
        public Stand? LongestStand { get; set; }
        public Stand? ShortestStand { get; set; }
    }
}
