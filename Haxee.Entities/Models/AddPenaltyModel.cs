namespace Haxee.Entities.Models
{
    /// <summary>
    /// Model, do ktorého sa mapujú dáta pri pridávani penalty na stanovisku.
    /// </summary>
    public class AddPenaltyModel
    {
        [Required]
        [Display(Name = "Penalty [min]")]
        public int PenaltyInMinutes { get; set; }
    }
}
