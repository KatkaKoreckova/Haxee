namespace Haxee.Entities.Models
{
    /// <summary>
    /// Model, do ktorého sa mapujú dáta pri vytváraní zariadenia.
    /// </summary>
    public class CreateDeviceModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Názov")]
        public string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Identifier")]
        public string Identifier { get; set; } = string.Empty;
    }
}
