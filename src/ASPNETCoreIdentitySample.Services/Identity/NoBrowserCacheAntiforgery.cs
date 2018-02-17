using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ASPNETCoreIdentitySample.Common.WebToolkit;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Antiforgery.Internal;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace ASPNETCoreIdentitySample.Services.Identity
{
    /// <summary>
    /// More info: https://github.com/aspnet/Antiforgery/issues/116
    /// </summary>
    public class NoBrowserCacheAntiforgery : IAntiforgery
    {
        private readonly DefaultAntiforgery _defaultAntiforgery;

        public NoBrowserCacheAntiforgery(IOptions<AntiforgeryOptions> antiforgeryOptionsAccessor,
            IAntiforgeryTokenGenerator tokenGenerator,
            IAntiforgeryTokenSerializer tokenSerializer,
            IAntiforgeryTokenStore tokenStore,
            ILoggerFactory loggerFactory)
        {
            _defaultAntiforgery = new DefaultAntiforgery(antiforgeryOptionsAccessor,
                tokenGenerator,
                tokenSerializer,
                tokenStore,
                loggerFactory);
        }

        public AntiforgeryTokenSet GetAndStoreTokens(HttpContext httpContext)
        {
            var result = _defaultAntiforgery.GetAndStoreTokens(httpContext);
            httpContext.DisableBrowserCache();
            return result;
        }

        public AntiforgeryTokenSet GetTokens(HttpContext httpContext)
        {
            return _defaultAntiforgery.GetTokens(httpContext);
        }

        public Task<bool> IsRequestValidAsync(HttpContext httpContext)
        {
            return _defaultAntiforgery.IsRequestValidAsync(httpContext);
        }

        public Task ValidateRequestAsync(HttpContext httpContext)
        {
            return _defaultAntiforgery.ValidateRequestAsync(httpContext);
        }

        public void SetCookieTokenAndHeader(HttpContext httpContext)
        {
            _defaultAntiforgery.SetCookieTokenAndHeader(httpContext);
        }
    }

    public class NoBrowserCacheHtmlGenerator : DefaultHtmlGenerator
    {
        public NoBrowserCacheHtmlGenerator(
            IAntiforgery antiforgery,
            IOptions<MvcViewOptions> optionsAccessor,
            IModelMetadataProvider metadataProvider,
            IUrlHelperFactory urlHelperFactory,
            HtmlEncoder htmlEncoder,
            ValidationHtmlAttributeProvider validationAttributeProvider)
            : base(
                antiforgery,
                optionsAccessor,
                metadataProvider,
                urlHelperFactory,
                htmlEncoder,
                validationAttributeProvider)
        {
        }

        public override IHtmlContent GenerateAntiforgery(ViewContext viewContext)
        {
            var result = base.GenerateAntiforgery(viewContext);

            // disable caching for the browser back button
            viewContext
                .HttpContext
                .Response
                .Headers[HeaderNames.CacheControl]
                    = "no-cache, max-age=0, must-revalidate, no-store";

            return result;
        }
    }
}