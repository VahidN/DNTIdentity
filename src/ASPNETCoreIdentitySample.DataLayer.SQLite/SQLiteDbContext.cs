using ASPNETCoreIdentitySample.Common.EFCoreToolkit;
using ASPNETCoreIdentitySample.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreIdentitySample.DataLayer.SQLite;

public class SQLiteDbContext(DbContextOptions options) : ApplicationDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // NOTE: Add custom SQLite's settings here ...

        builder.AddDateTimeOffsetConverter();
        builder.SetCaseInsensitiveSearchesForSQLite();
    }
}