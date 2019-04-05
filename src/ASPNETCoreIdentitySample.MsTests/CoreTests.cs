using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using Microsoft.AspNetCore.Hosting.Internal;
using ASPNETCoreIdentitySample.IocConfig;
using DNTCommon.Web.Core;

namespace ASPNETCoreIdentitySample.MsTests
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2510
    /// </summary>
    [TestClass]
    public class CoreTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptionsSnapshot<SiteSettings> _siteSettings;

        public CoreTests()
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddScoped<IHostingEnvironment, HostingEnvironment>();

            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
                                .Build();
            services.AddSingleton<IConfigurationRoot>(provider => configuration);
            services.Configure<SiteSettings>(options => configuration.Bind(options));

            // Adds all of the ASP.NET Core Identity related services and configurations at once.
            services.AddCustomIdentityServices();
            services.AddDNTCommonWeb();
            services.AddCloudscribePagination();

            var siteSettings = services.GetSiteSettings();
            siteSettings.ActiveDatabase = ActiveDatabase.InMemoryDatabase;
            services.AddRequiredEfInternalServices(siteSettings); // It's added to access services from the dbcontext, remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.
            services.AddDbContextPool<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.SetDbContextOptions(siteSettings);
                optionsBuilder.UseInternalServiceProvider(serviceProvider); // It's added to access services from the dbcontext, remove it if you are using the normal `AddDbContext` and normal constructor dependency injection.
            });

            _serviceProvider = services.BuildServiceProvider();

            _siteSettings = _serviceProvider.GetRequiredService<IOptionsSnapshot<SiteSettings>>();
            _siteSettings.Value.ActiveDatabase = ActiveDatabase.InMemoryDatabase;

            var identityDbInitialize = _serviceProvider.GetRequiredService<IIdentityDbInitializer>();
            identityDbInitialize.SeedData();
        }

        [TestMethod]
        public void Test_UserAdmin_Exists()
        {
            _serviceProvider.RunScopedService<IUnitOfWork>(context =>
            {
                var users = context.Set<User>();
                Assert.IsTrue(users.Any(x => x.UserName == "Admin"));
            });
        }
    }
}