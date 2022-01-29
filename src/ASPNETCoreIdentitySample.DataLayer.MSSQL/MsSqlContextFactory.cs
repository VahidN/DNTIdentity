using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.DataLayer.MSSQL;

public class MsSqlContextFactory : IDesignTimeDbContextFactory<MsSqlDbContext>
{
    public MsSqlDbContext CreateDbContext(string[] args)
    {
        var services = new ServiceCollection();
        services.AddOptions();
        services.AddLogging(cfg => cfg.AddConsole().AddDebug());
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<ILoggerFactory, LoggerFactory>();
        services.AddSingleton<AuditableEntitiesInterceptor>();

        var basePath = Directory.GetCurrentDirectory();
        WriteLine($"Using `{basePath}` as the ContentRootPath");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        services.AddSingleton(provider => configuration);
        services.Configure<SiteSettings>(options => configuration.Bind(options));

        var serviceProvider = services.BuildServiceProvider();
        var siteSettings = serviceProvider.GetRequiredService<IOptionsSnapshot<SiteSettings>>();
        siteSettings.Value.ActiveDatabase = ActiveDatabase.LocalDb;

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseConfiguredMsSql(siteSettings.Value, serviceProvider);

        return new MsSqlDbContext(optionsBuilder.Options);
    }
}