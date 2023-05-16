namespace Haxee.Entities.Models
{
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
