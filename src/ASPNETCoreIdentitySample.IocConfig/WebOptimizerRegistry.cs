using Microsoft.Extensions.DependencyInjection;

namespace ASPNETCoreIdentitySample.IocConfig;

public static class WebOptimizerRegistry
{
    public static void AddWebOptimizerServices(this IServiceCollection services)
        => services.AddWebOptimizer(pipeline =>
        {
            // Creates a CSS and a JS bundle. Globbing patterns supported.
            pipeline.AddCssBundle(route: "/css/site.min.css", "wwwroot/lib/bootstrap/dist/css/bootstrap.min.css",
                    "wwwroot/lib/bootstrap4-rtl/bootstrap-rtl.css",
                    "wwwroot/lib/components-font-awesome/css/solid.min.css",
                    "wwwroot/lib/components-font-awesome/css/fontawesome.min.css", "wwwroot/content/custom.css")
                .AdjustRelativePaths()
                .UseContentRoot();

            pipeline.AddJavaScriptBundle(route: "/js/site.min.js", "wwwroot/lib/jquery/dist/jquery.min.js",
                    "wwwroot/lib/popperjs/dist/umd/popper.min.js",
                    "wwwroot/lib/jquery-validation/dist/jquery.validate.min.js",
                    "wwwroot/lib/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js",
                    "wwwroot/lib/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.min.js",
                    "wwwroot/lib/bootstrap/dist/js/bootstrap.min.js",
                    "wwwroot/scripts/jquery.bootstrap-modal-confirm.js",
                    "wwwroot/scripts/jquery.bootstrap-modal-alert.js",
                    "wwwroot/scripts/jquery.bootstrap-modal-ajax-form.js", "wwwroot/scripts/custom.js")
                .UseContentRoot();

            // This will minify any JS and CSS file that isn't part of any bundle
            pipeline.MinifyCssFiles();
            pipeline.MinifyJsFiles();
        });
}