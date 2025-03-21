namespace Haxee.Internal.Data
{
    public class DataSeeder
    {
        /// <summary>
        /// Funkcia pre automatické napĺňanie databázy dátami.
        /// </summary>
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
                YearValue = DateTime.Now.Year,
                Status = Entities.Enums.YearStatus.Pending
            };

            db.Years.Add(year);

            var attendee = new Attendee
            {
                UserId = participant1.Id,
                YearId = year.Id,
                CardId = "e3:2e:98:a1"
            };

            db.Attendees.Add(attendee);

            var attendee2 = new Attendee
            {
                UserId = participant2.Id,
                YearId = year.Id,
                CardId = "d3:e0:61:1a"
            };

            db.Attendees.Add(attendee2);

            var stand = new Stand 
            { 
                Location = "Test lokacia ", 
                Name = "Test stanovisko",
                Number = 1,
                Penalty = TimeSpan.FromSeconds(60*5),
                YearId = year.Id,
                Capacity = 1
            };

            var quizStand = new Stand
            {
                Location = "Test kviz",
                Name = "Test kviz",
                Number = 2,
                Penalty = TimeSpan.FromSeconds(60),
                YearId = year.Id,
                IsQuiz = true,
                QuestionsAndAnswers = new()
                {
                    QuizHelper.GetQuestionAnswerPair("Testing question?", "Answer"),
                    QuizHelper.GetQuestionAnswerPair("Testing question2?", "Answer2")
                },
                Capacity = 1
            };

            db.Stands.Add(stand);
            db.Stands.Add(quizStand);

            await db.SaveChangesAsync();
        }
    }
}
