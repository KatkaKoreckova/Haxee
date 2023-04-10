namespace Haxee.Entities.Models
{
    public class ChangeSettingsModel
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; } = string.Empty;
        
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
