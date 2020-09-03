using System;
using ASPNETCoreIdentitySample.DataLayer.InMemoryDatabase;
using ASPNETCoreIdentitySample.DataLayer.MSSQL;
using ASPNETCoreIdentitySample.DataLayer.SQLite;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTCommon.Web.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNETCoreIdentitySample.IocConfig
{
    public static class DbContextOptionsExtensions
    {
        public static IServiceCollection AddConfiguredDbContext(
            this IServiceCollection serviceCollection, SiteSettings siteSettings)
        {
            switch (siteSettings.ActiveDatabase)
            {
                case ActiveDatabase.InMemoryDatabase:
                    serviceCollection.AddConfiguredInMemoryDbContext(siteSettings);
                    break;

                case ActiveDatabase.LocalDb:
                case ActiveDatabase.SqlServer:
                    serviceCollection.AddConfiguredMsSqlDbContext(siteSettings);
                    break;

                case ActiveDatabase.SQLite:
                    serviceCollection.AddConfiguredSQLiteDbContext(siteSettings);
                    break;

                default:
                    throw new NotSupportedException("Please set the ActiveDatabase in appsettings.json file.");
            }

            return serviceCollection;
        }

        /// <summary>
        /// Creates and seeds the database.
        /// </summary>
        public static void InitializeDb(this IServiceProvider serviceProvider)
        {
            serviceProvider.RunScopedService<IIdentityDbInitializer>(identityDbInitialize =>
            {
                identityDbInitialize.Initialize();
                identityDbInitialize.SeedData();
            });
        }
    }
}