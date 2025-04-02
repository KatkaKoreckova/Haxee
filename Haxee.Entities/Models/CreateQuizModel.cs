namespace Haxee.Entities.Models
{
    /// <summary>
    /// Model, do ktorého sa mapujú dáta pri vytváraní stanoviska typu kvíz.
    /// </summary>
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
        [Display(Name = "Maximálny počet ľudí")]
        [Range(1, Constants.Limits.MAX_PEOPLE)]
        public int Capacity { get; set; } = Constants.Limits.MAX_PEOPLE;

        [Required]
        [Display(Name = "Časový limit [min]")]
        [Range(1, Constants.Limits.MAX_STAND_TIME)]
        public int TimeLimitInMinutes { get; set; } = 10;

        [Required]
        [Display(Name = $"Otázky a odpovede (formát: otázka {Constants.QA_SEPARATOR} odpoveď)")]
        public string QuestionsAndAnswers { get; set; } = string.Empty;
    }
}
