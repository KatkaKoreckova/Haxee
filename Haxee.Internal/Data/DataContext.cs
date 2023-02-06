using Microsoft.EntityFrameworkCore;

namespace Haxee.Internal.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions dbContextOptions) : base (dbContextOptions)
        {

        }
    }
}
