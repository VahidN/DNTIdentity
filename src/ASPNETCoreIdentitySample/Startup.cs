using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.Common.PersianToolkit;
using ASPNETCoreIdentitySample.Common.WebToolkit;
using ASPNETCoreIdentitySample.Services.Identity.Logger;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using ASPNETCoreIdentitySample.IocConfig;
using ASPNETCoreIdentitySample.DataLayer.Context;

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
                options.UseCustomStringModelBinder();
                options.AllowEmptyInputInBodyModelBinding = true;
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddRazorViewRenderer();
            services.AddDNTCaptcha();
            services.AddMvcActionsDiscoveryService();
            services.AddProtectionProviderService();
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
            app.UseFileServer();

            app.UseFileServer(new FileServerOptions
            {
                // Set root of file server
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "bower_components")),
                // Only react to requests that match this path
                RequestPath = "/bower_components",
                // Don't expose file system
                EnableDirectoryBrowsing = false
            });

            // Adds all of the ASP.NET Core Identity related initializations at once.
            app.UseCustomIdentityServices();

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