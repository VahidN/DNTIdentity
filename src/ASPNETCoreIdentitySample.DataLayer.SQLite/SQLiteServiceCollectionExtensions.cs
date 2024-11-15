using ASPNETCoreIdentitySample.Common.PersianToolkit;
using ASPNETCoreIdentitySample.Common.WebToolkit;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreIdentitySample.DataLayer.SQLite;

public static class SQLiteServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguredSQLiteDbContext(this IServiceCollection services,
        SiteSettings siteSettings)
    {
        services.AddScoped<IUnitOfWork>(serviceProvider =>
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            SetCascadeOnSaveChanges(context);

            return context;
        });

        services.AddDbContextPool<ApplicationDbContext, SQLiteDbContext>((serviceProvider, optionsBuilder)
            => optionsBuilder.UseConfiguredSQLite(siteSettings, serviceProvider));

        return services;
    }

    private static void SetCascadeOnSaveChanges(ApplicationDbContext context)
    {
        // To fix https://github.com/dotnet/efcore/issues/19786
        context.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        context.ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
    }

    public static void UseConfiguredSQLite(this DbContextOptionsBuilder optionsBuilder,
        SiteSettings siteSettings,
        IServiceProvider serviceProvider)
    {
        if (optionsBuilder == null)
        {
            throw new ArgumentNullException(nameof(optionsBuilder));
        }

        if (siteSettings == null)
        {
            throw new ArgumentNullException(nameof(siteSettings));
        }

        var connectionString = siteSettings.GetSQLiteDbConnectionString();

        optionsBuilder.UseSqlite(connectionString, sqlServerOptionsBuilder =>
        {
            sqlServerOptionsBuilder.CommandTimeout((int)TimeSpan.FromMinutes(minutes: 3).TotalSeconds);
            sqlServerOptionsBuilder.MigrationsAssembly(typeof(SQLiteServiceCollectionExtensions).Assembly.FullName);
        });

        optionsBuilder.AddInterceptors(new PersianYeKeCommandInterceptor(),
            serviceProvider.GetRequiredService<AuditableEntitiesInterceptor>());

        optionsBuilder.ConfigureWarnings(warnings =>
        {
            warnings.Log((CoreEventId.LazyLoadOnDisposedContextWarning, LogLevel.Warning),
                (CoreEventId.DetachedLazyLoadingWarning, LogLevel.Warning),
                (CoreEventId.ManyServiceProvidersCreatedWarning, LogLevel.Warning),
                (CoreEventId.SensitiveDataLoggingEnabledWarning, LogLevel.Information));
        });

        optionsBuilder.EnableSensitiveDataLogging().EnableDetailedErrors();
    }

    public static string GetSQLiteDbConnectionString(this SiteSettings siteSettingsValue)
    {
        if (siteSettingsValue == null)
        {
            throw new ArgumentNullException(nameof(siteSettingsValue));
        }

        return siteSettingsValue.ActiveDatabase switch
        {
            ActiveDatabase.SQLite => siteSettingsValue.ConnectionStrings.SQLite.ApplicationDbContextConnection
                .ReplaceDataDirectoryInConnectionString(),
            _ => throw new NotSupportedException(
                message: "Please set the ActiveDatabase in appsettings.json file to `SQLite`.")
        };
    }

    public static string ReplaceDataDirectoryInConnectionString(this string connectionString)
    {
        if (connectionString == null)
        {
            return null;
        }

        return connectionString.Replace(oldValue: "|DataDirectory|", ServerInfo.GetAppDataFolderPath(),
            StringComparison.OrdinalIgnoreCase);
    }
}