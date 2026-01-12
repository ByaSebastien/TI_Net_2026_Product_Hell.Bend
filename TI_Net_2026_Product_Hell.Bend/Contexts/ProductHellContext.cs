using Microsoft.EntityFrameworkCore;
using TI_Net_2026_Product_Hell.Bend.Entities;

namespace TI_Net_2026_Product_Hell.Bend.Contexts
{
    public class ProductHellContext : DbContext
    {

        public DbSet<Product> Products => Set<Product>();

        public ProductHellContext(DbContextOptions<ProductHellContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductHellContext).Assembly);
        }
    }
}
