using Juan.Models;
using Microsoft.EntityFrameworkCore;

namespace Juan.Data
{
    public class JuanDbContext : DbContext
    {
        public DbSet<Setting> Settings { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public JuanDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
