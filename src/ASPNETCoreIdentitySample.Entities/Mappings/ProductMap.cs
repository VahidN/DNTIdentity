using ASPNETCoreIdentitySample.FluentAPIBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASPNETCoreIdentitySample.Entities.Mappings
{
    public class ProductMap : EntityMappingConfiguration<Product>
    {
        public override void Map(EntityTypeBuilder<Product> b)
        {
            // Primary Key
            b.HasKey(t => t.Id);

            // Properties

            // Table & Column Mappings
            b.ToTable("Products");
        }
    }
}
