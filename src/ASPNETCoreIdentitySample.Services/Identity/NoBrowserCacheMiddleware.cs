using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Http;

namespace ASPNETCoreIdentitySample.Services.Identity;

public class NoBrowserCacheMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context)
    {
        context.DisableBrowserCache();

        return next(context);
    }
}