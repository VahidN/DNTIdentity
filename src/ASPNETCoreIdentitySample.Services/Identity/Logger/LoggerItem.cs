using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using ASPNETCoreIdentitySample.Entities.Identity;

namespace ASPNETCoreIdentitySample.Services.Identity.Logger;

public class LoggerItem
{
    public AppShadowProperties Props { set; get; }
    public AppLogItem AppLogItem { set; get; }
}