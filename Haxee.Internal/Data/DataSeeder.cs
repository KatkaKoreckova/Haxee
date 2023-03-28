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
                CardId = "1",
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
                CardId = "2",
                UserName = Constants.Emails.INSTRUCTOR
            }, "jozef123");

            await userManager.CreateAsync(new User
            {
                Name = "Jozef Ucastnik",
                UserType = Entities.Enums.UserType.Kid,
                Email = Constants.Emails.KID,
                EmailConfirmed = true,
                CardId = "3",
                UserName = Constants.Emails.KID
            }, "jozef123");

            await userManager.CreateAsync(new User
            {
                Name = "Jozef Ucastnik 2",
                UserType = Entities.Enums.UserType.Kid,
                Email = Constants.Emails.KID2,
                EmailConfirmed = true,
                CardId = "4",
                UserName = Constants.Emails.KID2
            }, "jozef123");

            var dataContext = serviceProvider.GetRequiredService<DataContext>();

            dataContext.HifiArchives.Add(new HifiArchive
            {
                Year = 2022,
                Stands = Stands(1),
                Leaderboard = HifiLeaderboardPeople(2)
            });

            dataContext.HifiArchives.Add(new HifiArchive
            {
                Year = 2021,
                Stands = Stands(2),
                Leaderboard = HifiLeaderboardPeople(1)
            });

            await dataContext.SaveChangesAsync();
        }

        private static List<Stand> Stands(int n)
        {
            List<Stand> stands = new();

            stands.Add(new Stand
            {
                Name = "Blacbox",
                Location = "recepcia"
            });

            if (n == 1)
                return stands;

            stands.Add(new Stand
            {
                Name = "Sport",
                Location = "za budovou"
            });

            return stands;
        }

        private static List<HifiLeaderboardPerson> HifiLeaderboardPeople(int n)
        {
            List<HifiLeaderboardPerson> hifiLeaderboardPeople = new();

            hifiLeaderboardPeople.Add(new HifiLeaderboardPerson
            {
                Name = "Fero",
                Time = TimeSpan.FromMinutes(98),
                FinishedStands = 3,
                NotFinishedStands = 1,
                SkipedStands = 1,
                Penalty = TimeSpan.FromMinutes(5)
            });

            hifiLeaderboardPeople.Add(new HifiLeaderboardPerson
            {
                Name = "Jozef",
                Time = TimeSpan.FromMinutes(61),
                FinishedStands = 5,
                NotFinishedStands = 0,
                SkipedStands = 0,
                Penalty = TimeSpan.FromMinutes(0)
            });

            if (n == 1)
                return hifiLeaderboardPeople;

            hifiLeaderboardPeople.Add(new HifiLeaderboardPerson
            {
                Name = "Samo",
                Time = TimeSpan.FromMinutes(127),
                FinishedStands = 1,
                NotFinishedStands = 2,
                SkipedStands = 2,
                Penalty = TimeSpan.FromMinutes(15)
            });

            return hifiLeaderboardPeople;
        }
    }
}
