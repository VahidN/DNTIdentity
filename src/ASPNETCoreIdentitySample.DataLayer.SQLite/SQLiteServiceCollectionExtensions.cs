using System;
using ASPNETCoreIdentitySample.Common.PersianToolkit;
using ASPNETCoreIdentitySample.Common.WebToolkit;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNETCoreIdentitySample.DataLayer.SQLite
{
    public static class SQLiteServiceCollectionExtensions
    {
        public static IServiceCollection AddConfiguredSQLiteDbContext(this IServiceCollection services,
            SiteSettings siteSettings)
        {
            services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<ApplicationDbContext>());
            services.AddEntityFrameworkSqlite(); // It's added to access services from the dbcontext, remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.
            services.AddDbContextPool<ApplicationDbContext, SQLiteDbContext>(
                (serviceProvider, optionsBuilder) => optionsBuilder.UseConfiguredSQLite(siteSettings, serviceProvider));
            return services;
        }

        public static void UseConfiguredSQLite(
            this DbContextOptionsBuilder optionsBuilder, SiteSettings siteSettings, IServiceProvider serviceProvider)
        {
            var connectionString = siteSettings.GetSQLiteDbConnectionString();
            optionsBuilder.UseSqlite(
                connectionString,
                sqlServerOptionsBuilder =>
                {
                    sqlServerOptionsBuilder.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
                    sqlServerOptionsBuilder.MigrationsAssembly(typeof(SQLiteServiceCollectionExtensions).Assembly
                        .FullName);
                });
            optionsBuilder
                .UseInternalServiceProvider(
                    serviceProvider); // It's added to access services from the dbcontext, remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.
            optionsBuilder.AddInterceptors(new PersianYeKeCommandInterceptor());
            optionsBuilder.ConfigureWarnings(warnings =>
            {
                // ...
            });
        }

        public static string GetSQLiteDbConnectionString(this SiteSettings siteSettingsValue)
        {
            if (siteSettingsValue == null)
            {
                throw new ArgumentNullException(nameof(siteSettingsValue));
            }

            switch (siteSettingsValue.ActiveDatabase)
            {
                case ActiveDatabase.SQLite:
                    return siteSettingsValue.ConnectionStrings
                        .SQLite
                        .ApplicationDbContextConnection
                        .ReplaceDataDirectoryInConnectionString();

                default:
                    throw new NotSupportedException(
                        "Please set the ActiveDatabase in appsettings.json file to `SQLite`.");
            }
        }

        public static string ReplaceDataDirectoryInConnectionString(this string connectionString)
        {
            return connectionString.Replace("|DataDirectory|", ServerInfo.GetAppDataFolderPath());
        }
    }
}