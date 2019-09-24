using ASPNETCoreIdentitySample.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASPNETCoreIdentitySample.DataLayer.Mappings
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(product => product.Name).HasMaxLength(450).IsRequired();
            builder.HasOne(product => product.Category)
                   .WithMany(category => category.Products);
        }
    }
}