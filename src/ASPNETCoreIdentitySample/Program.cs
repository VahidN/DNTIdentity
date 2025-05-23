﻿using ASPNETCoreIdentitySample.IocConfig;
using ASPNETCoreIdentitySample.Services.Identity.Logger;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;

var builder = WebApplication.CreateBuilder(args);
ConfigureLogging(builder.Logging, builder.Environment, builder.Configuration);
ConfigureServices(builder.Services, builder.Configuration, builder.Environment);
var webApp = builder.Build();
ConfigureMiddlewares(webApp, webApp.Environment);
ConfigureEndpoints(webApp);
ConfigureDatabase(webApp);
await webApp.RunAsync();

void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
{
    services.Configure<SiteSettings>(configuration.Bind);

    services.Configure<ContentSecurityPolicyConfig>(options
        => configuration.GetSection(key: "ContentSecurityPolicyConfig").Bind(options));

    // Adds all of the ASP.NET Core Identity related services and configurations at once.
    services.AddCustomIdentityServices(configuration);

    services.AddMvc(options => options.UseYeKeModelBinder());

    services.AddDNTCommonWeb();

    services.AddDNTCaptcha(options =>
    {
        options.UseCookieStorageProvider()
            .AbsoluteExpiration(minutes: 7)
            .ShowExceptionsInResponse(env.IsDevelopment())
            .ShowThousandsSeparators(show: false)
            .WithEncryptionKey(key: "This is my secure key!");
    });

    services.AddCloudscribePagination();
    services.AddWebOptimizerServices();

    services.AddControllersWithViews(options => { options.Filters.Add<ApplyCorrectYeKeFilterAttribute>(); });
    services.AddRazorPages();
}

void ConfigureLogging(ILoggingBuilder logging, IHostEnvironment env, IConfiguration configuration)
{
    logging.ClearProviders();

    if (env.IsDevelopment())
    {
        logging.AddDebug();
        logging.AddConsole();
    }

    logging.AddConfiguration(configuration.GetSection(key: "Logging"));
    logging.AddDbLogger(); // You can change its Log Level using the `appsettings.json` file -> Logging -> LogLevel -> Default
}

void ConfigureMiddlewares(IApplicationBuilder app, IHostEnvironment env)
{
    if (!env.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseWebOptimizer();

    app.UseHttpsRedirection();
    app.UseExceptionHandler(errorHandlingPath: "/error/index/500");
    app.UseStatusCodePagesWithReExecute(pathFormat: "/error/index/{0}");

    app.UseContentSecurityPolicy();

    app.UseStaticFiles();

    app.UseRouting();
    app.UseRateLimiter();

    app.UseAuthentication();
    app.UseAuthorization();
}

void ConfigureEndpoints(IApplicationBuilder app)
    => app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();

        endpoints.MapControllerRoute(name: "areaRoute",
            pattern: "{area:exists}/{controller=Account}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

        endpoints.MapRazorPages();
    });

void ConfigureDatabase(IApplicationBuilder app) => app.ApplicationServices.InitializeDb();