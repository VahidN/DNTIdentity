using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreIdentitySample.DataLayer.Configurations;

public static class IdentityMappings
{
    /// <summary>
    ///     Adds all of the ASP.NET Core Identity related mappings at once.
    ///     More info: http://www.dntips.ir/post/2577
    ///     and http://www.dntips.ir/post/2578
    /// </summary>
    public static void AddCustomIdentityMappings(this ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityMappings).Assembly);

        // IEntityTypeConfiguration's which have constructors with parameters
        modelBuilder.ApplyConfiguration(new AppSqlCacheConfiguration());
    }
}