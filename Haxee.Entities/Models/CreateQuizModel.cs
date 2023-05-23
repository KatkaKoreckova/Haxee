namespace Haxee.Entities.Models
{
    public class CreateQuizModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Názov")]
        public string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Miesto")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Penalta za zlú odpoveď [min]")]
        public int PenaltyInMinutes { get; set; } = 1;

        [Required]
        [Display(Name = $"Otázky a odpovede (formát: otázka {Constants.QA_SEPARATOR} odpoveď)")]
        public string QuestionsAndAnswers { get; set; } = string.Empty;
    }
}
