using Microsoft.AspNetCore.Hosting;
using ASPNETCoreIdentitySample.Services.Identity.Logger;
using ASPNETCoreIdentitySample.IocConfig;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreIdentitySample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Services.InitializeDb();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging((hostingContext, logging) =>
                               {
                                   logging.ClearProviders();

                                   logging.AddDebug();

                                   if (hostingContext.HostingEnvironment.IsDevelopment())
                                   {
                                       logging.AddConsole();
                                   }

                                   logging.AddDbLogger(); // You can change its Log Level using the `appsettings.json` file -> Logging -> LogLevel -> Default
                                   logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                               })
                              .UseStartup<Startup>();
                });
    }
}