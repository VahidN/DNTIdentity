using ASPNETCoreIdentitySample.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASPNETCoreIdentitySample.DataLayer.Configurations;

public class AppSqlCacheConfiguration : IEntityTypeConfiguration<AppSqlCache>
{
    public void Configure(EntityTypeBuilder<AppSqlCache> builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        // For Microsoft.Extensions.Caching.SqlServer
        builder.ToTable("AppSqlCache", "dbo");
        builder.HasIndex(e => e.ExpiresAtTime).HasDatabaseName("Index_ExpiresAtTime");
        builder.Property(e => e.Id).HasMaxLength(449);
        builder.Property(e => e.Value).IsRequired();
    }
}