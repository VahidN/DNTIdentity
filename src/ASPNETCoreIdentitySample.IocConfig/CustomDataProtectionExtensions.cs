using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.IocConfig
{
    public static class CustomDataProtectionExtensions
    {
        public static IServiceCollection AddCustomDataProtection(
            this IServiceCollection services, SiteSettings siteSettings)
        {
            services.AddScoped<IXmlRepository, DataProtectionKeyService>();
            services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(serviceProvider =>
            {
                return new ConfigureOptions<KeyManagementOptions>(options =>
                {
                    var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
                    using (var scope = scopeFactory.CreateScope())
                    {
                        options.XmlRepository = scope.ServiceProvider.GetService<IXmlRepository>();
                    }
                });
            });
            services
                .AddDataProtection()
                .SetDefaultKeyLifetime(siteSettings.CookieOptions.ExpireTimeSpan)
                .SetApplicationName(siteSettings.CookieOptions.CookieName)
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });

            return services;
        }
    }
}