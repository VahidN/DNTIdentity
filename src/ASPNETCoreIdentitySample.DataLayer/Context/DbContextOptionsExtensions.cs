using System;
using System.IO;
using System.Linq;
using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNETCoreIdentitySample.DataLayer.Context
{
    public static class DbContextOptionsExtensions
    {
        /// <summary>
        /// It's added to access services from the dbcontext.
        /// remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.
        /// </summary>
        public static void AddRequiredEfInternalServices(
            this IServiceCollection serviceCollection,
            SiteSettings siteSettings)
        {
            switch (siteSettings.ActiveDatabase)
            {
                case ActiveDatabase.InMemoryDatabase:
                    serviceCollection.AddEntityFrameworkInMemoryDatabase();
                    break;

                case ActiveDatabase.LocalDb:
                case ActiveDatabase.SqlServer:
                    serviceCollection.AddEntityFrameworkSqlServer();
                    break;

                default:
                    throw new NotSupportedException("Please set the ActiveDatabase in appsettings.json file.");
            }
        }

        public static void SetDbContextOptions(
            this DbContextOptionsBuilder optionsBuilder,
            SiteSettings siteSettings)
        {
            switch (siteSettings.ActiveDatabase)
            {
                case ActiveDatabase.InMemoryDatabase:
                    optionsBuilder.UseInMemoryDatabase(siteSettings.ConnectionStrings.LocalDb.InitialCatalog);
                    break;

                case ActiveDatabase.LocalDb:
                case ActiveDatabase.SqlServer:
                    optionsBuilder.UseSqlServer(
                        siteSettings.GetDbConnectionString()
                        , serverDbContextOptionsBuilder =>
                        {
                            var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                            serverDbContextOptionsBuilder.CommandTimeout(minutes);
                            serverDbContextOptionsBuilder.EnableRetryOnFailure();
                        });
                    break;

                default:
                    throw new NotSupportedException("Please set the ActiveDatabase in appsettings.json file.");
            }

            optionsBuilder.ConfigureWarnings(warnings =>
            {
                warnings.Log(CoreEventId.IncludeIgnoredWarning);
            });
        }

        public static string GetDbConnectionString(this SiteSettings siteSettingsValue)
        {
            siteSettingsValue.CheckArgumentIsNull(nameof(siteSettingsValue));

            switch (siteSettingsValue.ActiveDatabase)
            {
                case ActiveDatabase.InMemoryDatabase:
                    return null;

                case ActiveDatabase.LocalDb:
                    var attachDbFilename = siteSettingsValue.ConnectionStrings.LocalDb.AttachDbFilename;
                    var webRootPath = Path.Combine(AppContext.BaseDirectory.Split(new[] { "bin" }, StringSplitOptions.RemoveEmptyEntries).First(), "wwwroot");
                    var attachDbFilenamePath = Path.Combine(webRootPath, "App_Data", attachDbFilename);
                    var localDbInitialCatalog = siteSettingsValue.ConnectionStrings.LocalDb.InitialCatalog;
                    return $@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog={localDbInitialCatalog};AttachDbFilename={attachDbFilenamePath};Integrated Security=True;MultipleActiveResultSets=True;";

                case ActiveDatabase.SqlServer:
                    return siteSettingsValue.ConnectionStrings.SqlServer.ApplicationDbContextConnection;

                default:
                    throw new NotSupportedException("Please set the ActiveDatabase in appsettings.json file.");
            }
        }
    }
}