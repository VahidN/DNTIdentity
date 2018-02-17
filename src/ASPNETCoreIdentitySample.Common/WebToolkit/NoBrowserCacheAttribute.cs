using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace ASPNETCoreIdentitySample.Common.WebToolkit
{
    public class NoBrowserCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.DisableBrowserCache();
            base.OnResultExecuting(filterContext);
        }
    }

    public static class CacheManager
    {
        public static void DisableBrowserCache(this HttpContext httpContext)
        {
            // Note: https://docs.microsoft.com/en-us/aspnet/core/performance/caching/middleware
            // The Antiforgery system for generating secure tokens to prevent Cross-Site Request Forgery (CSRF)
            // attacks sets the Cache-Control and Pragma headers to no-cache so that responses aren't cached.
            // More info:
            // https://github.com/aspnet/Antiforgery/blob/dev/src/Microsoft.AspNetCore.Antiforgery/Internal/DefaultAntiforgery.cs#L381
            // https://github.com/aspnet/Antiforgery/issues/116
            // https://github.com/aspnet/Security/issues/1474
            // So ... the following settings won't work for the pages with normal forms with default settings.
            httpContext.Response.Headers[HeaderNames.CacheControl] =
                       new StringValues(new[] { "no-cache", "max-age=0", "must-revalidate", "no-store" });
            httpContext.Response.Headers[HeaderNames.Expires] = "-1";
            httpContext.Response.Headers[HeaderNames.Pragma] = "no-cache";
        }
    }
}