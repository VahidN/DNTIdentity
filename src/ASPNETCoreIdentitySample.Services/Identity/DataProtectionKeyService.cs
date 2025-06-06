using System.Collections.ObjectModel;
using System.Xml.Linq;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities.Identity;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/2717/
/// </summary>
public class DataProtectionKeyService(IServiceProvider serviceProvider, ILogger<DataProtectionKeyService> logger)
    : IXmlRepository
{
    private readonly ILogger<DataProtectionKeyService> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IServiceProvider _serviceProvider =
        serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    public IReadOnlyCollection<XElement> GetAllElements()
        => _serviceProvider.RunScopedService<IUnitOfWork, ReadOnlyCollection<XElement>>(context =>
        {
            var dataProtectionKeys = context.Set<AppDataProtectionKey>().AsNoTracking();

            return dataProtectionKeys.Select(key => TryParseKeyXml(key.XmlData, _logger)).ToList().AsReadOnly();
        });

    public void StoreElement(XElement element, string friendlyName)
        =>

            // We need a separate context to call its SaveChanges several times,
            // without using the current request's context and changing its internal state.
            _serviceProvider.RunScopedService<IUnitOfWork>(context =>
            {
                var dataProtectionKeys = context.Set<AppDataProtectionKey>();
                var entity = dataProtectionKeys.SingleOrDefault(k => k.FriendlyName == friendlyName);

                if (entity != null)
                {
                    entity.XmlData = element.ToString();
                    dataProtectionKeys.Update(entity);
                }
                else
                {
                    dataProtectionKeys.Add(new AppDataProtectionKey
                    {
                        FriendlyName = friendlyName,
                        XmlData = element.ToString(SaveOptions.DisableFormatting)
                    });
                }

                context.SaveChanges();
            });

    private static XElement TryParseKeyXml(string xml, ILogger logger)
    {
        try
        {
            return XElement.Parse(xml);
        }
        catch (Exception e)
        {
            logger.LogWarningMessage($"An exception occurred while parsing the key xml '{xml}': {e}.");

            return null;
        }
    }
}