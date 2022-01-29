using System.Security.Cryptography.X509Certificates;
using ASPNETCoreIdentitySample.Common.WebToolkit;
using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.IocConfig;

public static class CustomDataProtectionExtensions
{
    public static IServiceCollection AddCustomDataProtection(
        this IServiceCollection services, SiteSettings siteSettings)
    {
        if (siteSettings == null)
        {
            throw new ArgumentNullException(nameof(siteSettings));
        }

        services.AddSingleton<IXmlRepository, DataProtectionKeyService>();
        services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(serviceProvider =>
        {
            return new ConfigureOptions<KeyManagementOptions>(options =>
            {
                serviceProvider.RunScopedService<IXmlRepository>(xmlRepository =>
                    options.XmlRepository = xmlRepository);
            });
        });

        //var certificate = LoadCertificateFromFile(siteSettings)
        services
            .AddDataProtection()
            .SetDefaultKeyLifetime(siteSettings.DataProtectionOptions.DataProtectionKeyLifetime)
            .SetApplicationName(siteSettings.DataProtectionOptions.ApplicationName);
        //.ProtectKeysWithCertificate(certificate)

        return services;
    }

    private static X509Certificate2 LoadCertificateFromFile(SiteSettings siteSettings)
    {
        // NOTE:
        // You should check out the identity of your application pool and make sure
        // that the `Load user profile` option is turned on, otherwise the crypto susbsystem won't work.

        var certificate = siteSettings.DataProtectionX509Certificate;
        var fileName = Path.Combine(ServerInfo.GetAppDataFolderPath(), certificate.FileName);

        // For decryption the certificate must be in the certificate store. It's a limitation of how EncryptedXml works.
        using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadWrite);
            using (var x509Certificate2 =
                   new X509Certificate2(fileName, certificate.Password, X509KeyStorageFlags.Exportable))
            {
                store.Add(x509Certificate2);
            }
        }

        var cert = new X509Certificate2(
            fileName,
            certificate.Password,
            X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet
                                              | X509KeyStorageFlags.Exportable);
        // TODO: If you are getting `Keyset does not exist`, run `wwwroot\App_Data\make-cert.cmd` again.
        WriteLine($"cert private key: {cert.GetRSAPrivateKey()}");
        return cert;
    }
}