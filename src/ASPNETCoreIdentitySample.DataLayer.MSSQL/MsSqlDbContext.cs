using ASPNETCoreIdentitySample.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreIdentitySample.DataLayer.MSSQL;

public class MsSqlDbContext : ApplicationDbContext
{
    public MsSqlDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        base.OnModelCreating(builder);

        // NOTE: Add custom MSSQL's settings here ...
    }
}