namespace Haxee.Entities.Models
{
    public class AddPenaltyModel
    {
        [Required]
        [Display(Name = "Penalty [min]")]
        public int PenaltyInMinutes { get; set; }
    }
}
