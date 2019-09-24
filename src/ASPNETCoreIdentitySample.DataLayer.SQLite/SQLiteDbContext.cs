using ASPNETCoreIdentitySample.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using ASPNETCoreIdentitySample.Common.EFCoreToolkit;

namespace ASPNETCoreIdentitySample.DataLayer.SQLite
{
    public class SQLiteDbContext : ApplicationDbContext
    {
        public SQLiteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.AddDateTimeOffsetConverter();
        }
    }
}