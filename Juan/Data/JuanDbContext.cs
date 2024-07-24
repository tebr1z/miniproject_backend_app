using Microsoft.EntityFrameworkCore;

namespace Juan.Data
{
    public class JuanDbContext : DbContext
    {
        public JuanDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
