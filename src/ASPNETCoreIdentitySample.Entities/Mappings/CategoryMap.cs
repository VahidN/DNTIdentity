using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ASPNETCoreIdentitySample.FluentAPIBase;

namespace ASPNETCoreIdentitySample.Entities.Mappings
{
    public class CategoryMap : EntityMappingConfiguration<Category>
    {
        public override void Map(EntityTypeBuilder<Category> b)
        {
            // Primary Key
            b.HasKey(t => t.Id);

            // Properties

            // Table & Column Mappings
            b.ToTable("Categories");
        }
    }
}
