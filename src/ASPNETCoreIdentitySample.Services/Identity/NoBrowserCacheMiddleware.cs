using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Http;

namespace ASPNETCoreIdentitySample.Services.Identity;

public class NoBrowserCacheMiddleware
{
    private readonly RequestDelegate _next;

    public NoBrowserCacheMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        context.DisableBrowserCache();
        return _next(context);
    }
}