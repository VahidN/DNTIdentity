using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using DNTCommon.Web.Core;

namespace ASPNETCoreIdentitySample.Services.Identity
{
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

    public static class NoBrowserCacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseNoBrowserCache(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NoBrowserCacheMiddleware>();
        }
    }
}