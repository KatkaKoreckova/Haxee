namespace Haxee.Internal.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Stand> Stands { get; set; }
        public DataContext(DbContextOptions dbContextOptions) : base (dbContextOptions) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
