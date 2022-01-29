using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNETCoreIdentitySample.IocConfig;

public static class IdentityServicesRegistry
{
    /// <summary>
    ///     Adds all of the ASP.NET Core Identity related services and configurations at once.
    /// </summary>
    public static void AddCustomIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var siteSettings = GetSiteSettings(configuration);
        services.AddIdentityOptions(siteSettings);
        services.AddConfiguredDbContext(siteSettings);
        services.AddCustomServices();
        services.AddCustomTicketStore(siteSettings);
        services.AddDynamicPermissions();
        services.AddCustomDataProtection(siteSettings);
    }

    public static SiteSettings GetSiteSettings(this IConfiguration configuration)
    {
        return configuration.Get<SiteSettings>();
    }
}