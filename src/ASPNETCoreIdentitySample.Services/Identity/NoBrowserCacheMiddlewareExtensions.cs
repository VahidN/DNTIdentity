using Microsoft.AspNetCore.Builder;

namespace ASPNETCoreIdentitySample.Services.Identity;

public static class NoBrowserCacheMiddlewareExtensions
{
    public static IApplicationBuilder UseNoBrowserCache(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<NoBrowserCacheMiddleware>();
    }
}