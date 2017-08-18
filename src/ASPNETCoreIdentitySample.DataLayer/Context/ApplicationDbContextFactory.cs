using System;
using System.IO;
using System.Linq;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.DataLayer.Context
{
    public class CustomHostingEnvironment : IHostingEnvironment
    {
        public string EnvironmentName { get; set; } = "DataLayer.Context";
        public string ApplicationName { get; set; } = "ASPNETCoreIdentitySample";
        public string WebRootPath { get; set; } = Path.Combine(AppContext.BaseDirectory.Split(new[] { "bin" }, StringSplitOptions.RemoveEmptyEntries).First(), "wwwroot");
        public IFileProvider WebRootFileProvider { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory.Split(new[] { "bin" }, StringSplitOptions.RemoveEmptyEntries).First();
        public IFileProvider ContentRootFileProvider { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }

    /// <summary>
    /// Only used by EF Tooling
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddScoped<IHostingEnvironment, CustomHostingEnvironment>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            var serviceProvider = services.BuildServiceProvider();

            var hostingEnvironment = serviceProvider.GetRequiredService<IHostingEnvironment>();
            Console.WriteLine($"Using `{hostingEnvironment.ContentRootPath}` as the ContentRootPath");
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(hostingEnvironment.ContentRootPath)
                                .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
                                .Build();
            services.AddSingleton<IConfigurationRoot>(provider => configuration);
            services.Configure<SiteSettings>(options => configuration.Bind(options));
            serviceProvider = services.BuildServiceProvider();

            var siteSettings = serviceProvider.GetRequiredService<IOptionsSnapshot<SiteSettings>>();
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<ApplicationDbContextBase>();
            return new ApplicationDbContext(siteSettings, httpContextAccessor, hostingEnvironment, logger);
        }
    }
}