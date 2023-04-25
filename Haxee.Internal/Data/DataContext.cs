namespace Haxee.Internal.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Stand> Stands { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<StandVisit> StandVisits { get; set; }
        public DataContext(DbContextOptions dbContextOptions) : base (dbContextOptions) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
