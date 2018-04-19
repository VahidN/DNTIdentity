using ASPNETCoreIdentitySample.Services.Identity.Logger;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ASPNETCoreIdentitySample.IocConfig;
using ASPNETCoreIdentitySample.DataLayer.Context;
using DNTCommon.Web.Core;

namespace ASPNETCoreIdentitySample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(options => Configuration.Bind(options));

            // Adds all of the ASP.NET Core Identity related services and configurations at once.
            services.AddCustomIdentityServices();

            var siteSettings = services.GetSiteSettings();
            services.AddRequiredEfInternalServices(siteSettings); // It's added to access services from the dbcontext, remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.
            services.AddDbContextPool<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.SetDbContextOptions(siteSettings);
                optionsBuilder.UseInternalServiceProvider(serviceProvider); // It's added to access services from the dbcontext, remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.
            });

            services.AddMvc(options =>
            {
                options.UseYeKeModelBinder();
                options.AllowEmptyInputInBodyModelBinding = true;
                // options.Filters.Add(new NoBrowserCacheAttribute());
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddDNTCommonWeb();
            services.AddDNTCaptcha();
            services.AddCloudscribePagination();
        }

        public void Configure(
            ILoggerFactory loggerFactory,
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            loggerFactory.AddDbLogger(serviceProvider: app.ApplicationServices, minLevel: LogLevel.Warning);

            app.UseExceptionHandler("/error/index/500");
            app.UseStatusCodePagesWithReExecute("/error/index/{0}");

            // Serve wwwroot as root
            app.UseFileServer(new FileServerOptions
            {
                // Don't expose file system
                EnableDirectoryBrowsing = false
            });

            // Adds all of the ASP.NET Core Identity related initializations at once.
            app.UseCustomIdentityServices();

            // app.UseNoBrowserCache();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Account}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}