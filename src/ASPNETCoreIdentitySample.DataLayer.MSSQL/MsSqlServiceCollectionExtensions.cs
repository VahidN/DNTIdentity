using ASPNETCoreIdentitySample.Common.PersianToolkit;
using ASPNETCoreIdentitySample.Common.WebToolkit;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreIdentitySample.DataLayer.MSSQL;

public static class MsSqlServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguredMsSqlDbContext(this IServiceCollection services,
        SiteSettings siteSettings)
    {
        services.AddScoped<IUnitOfWork>(serviceProvider =>
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            SetCascadeOnSaveChanges(context);
            return context;
        });
        services.AddDbContextPool<ApplicationDbContext, MsSqlDbContext>(
            (serviceProvider, optionsBuilder) => optionsBuilder.UseConfiguredMsSql(siteSettings, serviceProvider));
        return services;
    }

    private static void SetCascadeOnSaveChanges(ApplicationDbContext context)
    {
        // To fix https://github.com/dotnet/efcore/issues/19786
        context.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        context.ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
    }

    public static void UseConfiguredMsSql(
        this DbContextOptionsBuilder optionsBuilder, SiteSettings siteSettings, IServiceProvider serviceProvider)
    {
        if (optionsBuilder == null)
        {
            throw new ArgumentNullException(nameof(optionsBuilder));
        }

        if (siteSettings == null)
        {
            throw new ArgumentNullException(nameof(siteSettings));
        }

        var connectionString = siteSettings.GetMsSqlDbConnectionString();
        optionsBuilder.UseSqlServer(
            connectionString,
            sqlServerOptionsBuilder =>
            {
                sqlServerOptionsBuilder.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
                sqlServerOptionsBuilder.EnableRetryOnFailure(10,
                    TimeSpan.FromSeconds(7),
                    null);
                sqlServerOptionsBuilder.MigrationsAssembly(typeof(MsSqlServiceCollectionExtensions).Assembly.FullName);
            });
        optionsBuilder.AddInterceptors(new PersianYeKeCommandInterceptor(),
            serviceProvider.GetRequiredService<AuditableEntitiesInterceptor>());
        optionsBuilder.ConfigureWarnings(warnings =>
        {
            warnings.Log(
                (CoreEventId.LazyLoadOnDisposedContextWarning, LogLevel.Warning),
                (CoreEventId.DetachedLazyLoadingWarning, LogLevel.Warning),
                (CoreEventId.ManyServiceProvidersCreatedWarning, LogLevel.Warning),
                (CoreEventId.SensitiveDataLoggingEnabledWarning, LogLevel.Information)
            );
        });
        optionsBuilder.EnableSensitiveDataLogging().EnableDetailedErrors();
    }

    public static string GetMsSqlDbConnectionString(this SiteSettings siteSettingsValue)
    {
        if (siteSettingsValue == null)
        {
            throw new ArgumentNullException(nameof(siteSettingsValue));
        }

        switch (siteSettingsValue.ActiveDatabase)
        {
            case ActiveDatabase.LocalDb:
                var attachDbFilename = siteSettingsValue.ConnectionStrings.LocalDb.AttachDbFilename;
                var attachDbFilenamePath = Path.Combine(ServerInfo.GetAppDataFolderPath(), attachDbFilename);
                var localDbInitialCatalog = siteSettingsValue.ConnectionStrings.LocalDb.InitialCatalog;
                return
                    $@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog={localDbInitialCatalog};AttachDbFilename={attachDbFilenamePath};Integrated Security=True;MultipleActiveResultSets=True;";

            case ActiveDatabase.SqlServer:
                return siteSettingsValue.ConnectionStrings.SqlServer.ApplicationDbContextConnection;

            default:
                throw new NotSupportedException(
                    "Please set the ActiveDatabase in appsettings.json file to `LocalDb` or `SqlServer`.");
        }
    }
}