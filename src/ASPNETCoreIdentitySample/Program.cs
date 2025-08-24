using ASPNETCoreIdentitySample.IocConfig;
using ASPNETCoreIdentitySample.Services.Identity.Logger;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

const string externalLoginErrorRedirectBase = "/Identity/Login?externalError=";
const string externalLoginErrorGoogle = "GoogleRemoteFailure";
const string externalLoginErrorMicrosoft = "MicrosoftRemoteFailure";
const string externalLoginErrorGitHub = "GitHubRemoteFailure";

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

    var authBuilder = services.AddAuthentication();
    ConfigureExternalProviders(authBuilder, configuration);

    services.AddCloudscribePagination();
    services.AddWebOptimizerServices();

    services.AddControllersWithViews(options => { options.Filters.Add<ApplyCorrectYeKeFilterAttribute>(); });
    services.AddRazorPages();
}

static void ConfigureExternalProviders(AuthenticationBuilder auth, IConfiguration configuration)
{
    static bool Has(string v) => !string.IsNullOrWhiteSpace(v);
    var siteSettings = configuration.Get<SiteSettings>();

    var googleClientId = siteSettings.Authentication.Google.ClientId;
    var googleClientSecret = siteSettings.Authentication.Google.ClientSecret;
    if (Has(googleClientId) && Has(googleClientSecret) && siteSettings.Authentication.Google.Enabled)
    {
        auth.AddGoogle(options =>
        {
            options.ClientId = googleClientId!;
            options.ClientSecret = googleClientSecret!;
            options.SaveTokens = true;
            options.Scope.Add("profile");
            options.Scope.Add("email");
            options.Events.OnRemoteFailure = ctx =>
            {
                // Avoid exposing raw ctx.Failure?.Message to end users
                ctx.Response.Redirect(externalLoginErrorRedirectBase + externalLoginErrorGoogle);
                ctx.HandleResponse();
                return Task.CompletedTask;
            };
        });
    }

    var msClientId = siteSettings.Authentication.Microsoft.ClientId;
    var msClientSecret = siteSettings.Authentication.Microsoft.ClientSecret;
    if (Has(msClientId) && Has(msClientSecret) && siteSettings.Authentication.Microsoft.Enabled)
    {
        auth.AddMicrosoftAccount(options =>
        {
            options.ClientId = msClientId!;
            options.ClientSecret = msClientSecret!;
            options.SaveTokens = true;
            options.Events.OnRemoteFailure = ctx =>
            {
                // Avoid exposing raw ctx.Failure?.Message to end users
                ctx.Response.Redirect(externalLoginErrorRedirectBase + externalLoginErrorMicrosoft);
                ctx.HandleResponse();
                return Task.CompletedTask;
            };
        });
    }

    var ghClientId = siteSettings.Authentication.GitHub.ClientId;
    var ghClientSecret = siteSettings.Authentication.GitHub.ClientSecret;
    if (Has(ghClientId) && Has(ghClientSecret) && siteSettings.Authentication.GitHub.Enabled)
    {
        auth.AddGitHub(options =>
        {
            options.ClientId = ghClientId!;
            options.ClientSecret = ghClientSecret!;
            options.SaveTokens = true;
            options.Scope.Add("read:user");
            options.Scope.Add("user:email");
            options.Events.OnRemoteFailure = ctx =>
            {
                // Avoid exposing raw ctx.Failure?.Message to end users
                ctx.Response.Redirect(externalLoginErrorRedirectBase + externalLoginErrorGitHub);
                ctx.HandleResponse();
                return Task.CompletedTask;
            };
        });
    }
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