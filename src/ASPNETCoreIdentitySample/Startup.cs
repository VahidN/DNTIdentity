using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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
using ASPNETCoreIdentitySample.Services.Token;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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
      services.Configure<BearerTokensOptions>(options => Configuration.GetSection("BearerTokens").Bind(options));
      services.Configure<ApiSettings>(options => Configuration.GetSection("ApiSettings").Bind(options));
      services.Configure<SiteSettings>(options => Configuration.Bind(options));
      services.AddScoped<IAntiForgeryCookieService, AntiForgeryCookieService>();
      // Adds all of the ASP.NET Core Identity related services and configurations at once.
      services.AddCustomIdentityServices();

      var siteSettings = services.GetSiteSettings();
      services.AddRequiredEfInternalServices(siteSettings); // It's added to access services from the dbcontext, remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.
      services.AddDbContextPool<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
      {
        optionsBuilder.SetDbContextOptions(siteSettings);
        optionsBuilder.UseInternalServiceProvider(serviceProvider); // It's added to access services from the dbcontext, remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.
      });



      services.AddAuthentication(options =>
      {
        //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;


        //  options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //  options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //  options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;


      })
           .AddCookie(cfg => cfg.SlidingExpiration = true)
       .AddJwtBearer(cfg =>
       {
         cfg.RequireHttpsMetadata = false;
         cfg.SaveToken = true;
         cfg.TokenValidationParameters = new TokenValidationParameters
         {
           ValidIssuer = Configuration["BearerTokens:Issuer"], // site that makes the token
           ValidateIssuer = false, // TODO: change this to avoid forwarding attacks
           ValidAudience = Configuration["BearerTokens:Audience"], // site that consumes the token
           ValidateAudience = false, // TODO: change this to avoid forwarding attacks
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["BearerTokens:Key"])),
           ValidateIssuerSigningKey = true, // verify signature to avoid tampering
           ValidateLifetime = true, // validate the expiration
           ClockSkew = TimeSpan.Zero // tolerance for the expiration date
         };
         cfg.Events = new JwtBearerEvents
         {
           OnAuthenticationFailed = context =>
           {
             var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
             logger.LogError("Authentication failed.", context.Exception);
             return Task.CompletedTask;
           },
           OnTokenValidated = context =>
           {
             var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
             return tokenValidatorService.ValidateAsync(context);
           },
           OnMessageReceived = context =>
           {
             return Task.CompletedTask;
           },
           OnChallenge = context =>
           {
             var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
             logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
             return Task.CompletedTask;
           }
         };
       });



      //services.AddCors(options =>
      //{
      //  options.AddPolicy("CorsPolicy",
      //    builder => builder
      //      .WithOrigins("https://localhost:5001") //Note:  The URL must be specified without a trailing slash (/).
      //      .AllowAnyMethod()
      //      .AllowAnyHeader()
      //      .AllowCredentials());
      //});

      //services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");


      services.AddMvc(options =>
      {
        //  options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        options.UseYeKeModelBinder();
        options.AllowEmptyInputInBodyModelBinding = true;
        // options.Filters.Add(new NoBrowserCacheAttribute());
      }).AddJsonOptions(jsonOptions =>
      {
        jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
      })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddDNTCommonWeb();
      services.AddDNTCaptcha();
      services.AddCloudscribePagination();
    }

    public void Configure(
        ILoggerFactory loggerFactory,
        IApplicationBuilder app,
        IHostingEnvironment env)
    {


      app.UseExceptionHandler(appBuilder =>
      {
        appBuilder.Use(async (context, next) =>
        {
          var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
          if (error != null && error.Error is SecurityTokenExpiredException)
          {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
              State = 401,
              Msg = "token expired"
            }));
          }
          else if (error != null && error.Error != null)
          {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
              State = 500,
              Msg = error.Error.Message
            }));
          }
          else
          {
            await next();
          }
        });
      });


      loggerFactory.AddDbLogger(serviceProvider: app.ApplicationServices, minLevel: LogLevel.Warning);

      if (!env.IsDevelopment())
      {
        app.UseHsts();
      }

      app.UseHttpsRedirection();
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
      app.UseCookiePolicy();

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


      // catch-all handler for HTML5 client routes - serve index.html
      app.Run(async context =>
      {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
      });



    }
  }
}