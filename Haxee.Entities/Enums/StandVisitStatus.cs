namespace Haxee.Entities.Enums
{
    /// <summary>
    /// Zoznam statusov, ktoré može súťažiaci na stanovisku mať.
    /// </summary>
    public enum StandVisitStatus
    {
        [Display(Name = "čaká")]
        Waiting,
        [Display(Name = "pracuje")]
        Working,
        [Display(Name = "dokončené")]
        Done
    }
}
