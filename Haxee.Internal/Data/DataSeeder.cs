using Microsoft.Extensions.DependencyInjection;

namespace Haxee.Internal.Data
{
    public class DataSeeder
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            await userManager.CreateAsync(new User
            {
                Name = "Jozef Veduci",
                UserType = Entities.Enums.UserType.Instructor,
                SuperInstructor = true,
                Email = "superjozef@lstme.sk",
                EmailConfirmed = true
            }, "jozef123");

            await userManager.CreateAsync(new User
            {
                Name = "Jozef Instruktor",
                UserType = Entities.Enums.UserType.Instructor,
                Email = "jozef@lstme.sk",
                EmailConfirmed = true
            }, "jozef123");

            await userManager.CreateAsync(new User
            {
                Name = "Jozef Ucastnik",
                UserType = Entities.Enums.UserType.Kid,
                Email = "ucastnik@lstme.sk",
                EmailConfirmed = true
            }, "jozef123");
        }
    }
}
