namespace Haxee.Internal.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Stand> Stands { get; set; }
        public DataContext(DbContextOptions dbContextOptions) : base (dbContextOptions) {}

        public DbSet<HifiArchive> HifiArchives { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<User>()
                .HasOne(x => x.CurrentStand)
                .WithMany(x => x.CurrentPeople)
                .HasForeignKey(x => x.CurrentStandId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.Entity<Stand>()
                .HasOne(x => x.Supervisor)
                .WithOne(x => x.SupervisorOfStand)
                .HasForeignKey<Stand>(x => x.SupervisorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
