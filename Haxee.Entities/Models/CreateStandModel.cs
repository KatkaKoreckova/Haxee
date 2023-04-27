namespace Haxee.Entities.Models
{
    public class CreateStandModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Názov")]
        public string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Miesto")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Penalty [min]")]
        public int PenaltyInMinutes { get; set; } = 10;

        public User? Supervisor { get; set; }
    }
}
