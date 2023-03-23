namespace Haxee.Entities.Models
{
    public class AddAccountModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string DateOfBirth { get; set; }

        public UserType UserType { get; set; } = UserType.Kid;

        public bool SuperInstructor { get; set; } = false;

    }
}
