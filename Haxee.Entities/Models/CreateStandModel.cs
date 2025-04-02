namespace Haxee.Entities.Models
{
    /// <summary>
    /// Model, do ktorého sa mapujú dáta pri vytváraní stanoviska.
    /// </summary>
    public class CreateStandModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Názov")]
        public string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Miesto")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Penalizácia [min]")]
        public int PenaltyInMinutes { get; set; } = 10;

        [Required]
        [Display(Name = "Časový limit [min]")]
        [Range(1, Constants.Limits.MAX_STAND_TIME)]
        public int TimeLimitInMinutes { get; set; } = 10;

        [Required]
        [Display(Name = "Maximálny počet ľudí")]
        [Range(1, Constants.Limits.MAX_PEOPLE)]
        public int Capacity { get; set; }

        public User? Supervisor { get; set; }

        public Device? Device { get; set; }
    }
}
