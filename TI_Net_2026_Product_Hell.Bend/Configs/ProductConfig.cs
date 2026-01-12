using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_Net_2026_Product_Hell.Bend.Entities;

namespace TI_Net_2026_Product_Hell.Bend.Configs
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product").HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.Image).IsRequired();
        }
    }
}
