using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ASPNETCoreIdentitySample.FluentAPIBase;

namespace ASPNETCoreIdentitySample.Entities.Mappings
{
    public class BaseFileMap : EntityMappingConfiguration<BaseFile>
    {
        public override void Map(EntityTypeBuilder<BaseFile> b)
        {
            // Primary Key
            b.HasKey(t => t.Id);

            // Properties
            b.Property(t => t.FileName)
                .IsRequired();

            b.Property(t => t.FileContentType)
                .IsRequired();

            b.Property(t => t.FileTypeFilter)
                .IsRequired();


            // Table & Column Mappings
            b.ToTable("SiteFiles");
            b.Property(t => t.Id).HasColumnName("Id");
            b.Property(t => t.FileGuid).HasColumnName("FileGuid");
            b.Property(t => t.FileName).HasColumnName("FileName");
            b.Property(t => t.FileContentType).HasColumnName("FileContentType");
            b.Property(t => t.FileTypeFilter).HasColumnName("FileTypeFilter");
            b.Property(t => t.FileOnDs).HasColumnName("FileOnDs");
            b.Property(t => t.FileSize).HasColumnName("FileSize");
        }
    }
}
