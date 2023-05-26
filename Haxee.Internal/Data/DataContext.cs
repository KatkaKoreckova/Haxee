using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Haxee.Internal.Data
{
    /// <summary>
    /// Určovanie tabuliek v databáze
    /// </summary>
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Stand> Stands { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<StandVisit> StandVisits { get; set; }
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var listStringConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, typeof(List<string>), Constants.DEFAULT_OPTIONS),
                v => JsonSerializer.Deserialize<List<string>>(v, Constants.DEFAULT_OPTIONS) ?? new List<string>()
            );

            builder.Entity<Stand>()
                .Property(x => x.QuestionsAndAnswers)
                .HasConversion(listStringConverter);
        }
    }
}
