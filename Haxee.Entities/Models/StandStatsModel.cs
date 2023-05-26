namespace Haxee.Entities.Models
{
    /// <summary>
    /// Model pre zobrazenie dát o tom, kde je najdlhšie a kde najkratšie čakanie na stanovisku.
    /// </summary>
    public class StandStatsModel
    {
        public Stand? LongestWaitStand { get; set; }
        public Stand? ShortestWaitStand { get; set; }
    }
}
