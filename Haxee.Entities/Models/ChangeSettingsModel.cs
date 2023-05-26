namespace Haxee.Entities.Models
{
    /// <summary>
    /// Model, do ktorého sa mapujú dáta pri zmene nastavení používateľského konta.
    /// </summary>
    public class ChangeSettingsModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Meno")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Priezvisko")]
        public string LastName { get; set; } = string.Empty;
        
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
