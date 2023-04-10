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

            var participant1 = new User
            {
                Name = "Jozef Ucastnik",
                UserType = Entities.Enums.UserType.Kid,
                Email = Constants.Emails.KID,
                EmailConfirmed = true,
                UserName = Constants.Emails.KID
            };

            var participant2 = new User
            {
                Name = "Jozef Ucastnik 2",
                UserType = Entities.Enums.UserType.Kid,
                Email = Constants.Emails.KID2,
                EmailConfirmed = true,
                UserName = Constants.Emails.KID2
            };

            await userManager.CreateAsync(new User
            {
                Name = "Jozef Instruktor",
                UserType = Entities.Enums.UserType.Instructor,
                Email = Constants.Emails.INSTRUCTOR,
                EmailConfirmed = true,
                UserName = Constants.Emails.INSTRUCTOR
            }, "jozef123");

            await userManager.CreateAsync(participant1, "jozef123");

            await userManager.CreateAsync(participant2, "jozef123");

            var db = serviceProvider.GetRequiredService<DataContext>()!;

            var year = new Year
            { 
                YearValue = DateTime.Now.Year
            };

            db.Years.Add(year);

            db.Attendees.Add(new Attendee 
            { 
                UserId = participant1.Id,
                YearId = year.Id
            });

            await db.SaveChangesAsync();
        }
    }
}
