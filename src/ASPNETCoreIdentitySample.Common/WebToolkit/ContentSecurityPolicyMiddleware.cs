using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ASPNETCoreIdentitySample.Common.WebToolkit
{
    public class ContentSecurityPolicyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _contentSecurityPolicyValue;
        private readonly IConfiguration _configuration;

        public ContentSecurityPolicyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _contentSecurityPolicyValue = getContentSecurityPolicyValue();
        }

        private string getContentSecurityPolicyValue()
        {
            var contentSecurityPolicyErrorLogUri = _configuration["ContentSecurityPolicyErrorLogUri"];
            if (string.IsNullOrWhiteSpace(contentSecurityPolicyErrorLogUri))
            {
                throw new NullReferenceException("Please set the `ContentSecurityPolicyErrorLogUri` value in `appsettings.json` file.");
            }

            string[] csp =
            {
              "default-src 'self' blob:",
              "style-src 'self' 'unsafe-inline'",
              "script-src 'self' https://freegeoip.net/ 'unsafe-inline' 'unsafe-eval' ",
              "font-src 'self'",
              "img-src 'self' data: blob:",
              "connect-src 'self'",
              "media-src 'self'",
              "object-src 'self' blob:",
              $"report-uri {contentSecurityPolicyErrorLogUri}"
            };
            return string.Join("; ", csp);
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff"); // Refused to execute script from '<URL>' because its MIME type ('') is not executable, and strict MIME type checking is enabled.
            context.Response.Headers.Add("Content-Security-Policy", _contentSecurityPolicyValue);
            return _next(context);
        }
    }

    public static class ContentSecurityPolicyMiddlewareExtensions
    {
        /// <summary>
        /// Make sure you add this code BEFORE app.UseStaticFiles();,
        /// otherwise the headers will not be applied to your static files.
        /// </summary>
        public static IApplicationBuilder UseContentSecurityPolicy(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ContentSecurityPolicyMiddleware>();
        }
    }
}