namespace Haxee.Entities.Models
{
    public class CreateStandModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public int PenaltyInSeconds { get; set; }
    }
}
