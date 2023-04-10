namespace Haxee.Internal.Data
{
    public class DataSeeder
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (await userManager.FindByEmailAsync(Constants.Emails.SUPER_ADMIN) is not null)
                return;

            var adminUser = new User
            {
                Name = "Jozef Veduci",
                UserType = Entities.Enums.UserType.Instructor,
                SuperInstructor = true,
                Email = Constants.Emails.SUPER_ADMIN,
                EmailConfirmed = true,
                UserName = Constants.Emails.SUPER_ADMIN
            };

            var creationResult = await userManager.CreateAsync(adminUser, "jozef123");

            if (creationResult.Succeeded)
            {
                await userManager.AddClaimsAsync(adminUser, new List<Claim> {
                    new Claim(ClaimTypes.Role, Constants.Roles.Admin),
                    new Claim(ClaimTypes.Role, Constants.Roles.User)
                });
            }

            await userManager.CreateAsync(new User
            {
                Name = "Jozef Instruktor",
                UserType = Entities.Enums.UserType.Instructor,
                Email = Constants.Emails.INSTRUCTOR,
                EmailConfirmed = true,
                UserName = Constants.Emails.INSTRUCTOR
            }, "jozef123");

            await userManager.CreateAsync(new User
            {
                Name = "Jozef Ucastnik",
                UserType = Entities.Enums.UserType.Kid,
                Email = Constants.Emails.KID,
                EmailConfirmed = true,
                UserName = Constants.Emails.KID
            }, "jozef123");

            await userManager.CreateAsync(new User
            {
                Name = "Jozef Ucastnik 2",
                UserType = Entities.Enums.UserType.Kid,
                Email = Constants.Emails.KID2,
                EmailConfirmed = true,
                UserName = Constants.Emails.KID2
            }, "jozef123");
        }
    }
}
