using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ASPNETCoreIdentitySample.Services.Identity.Logger;

namespace ASPNETCoreIdentitySample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.SetBasePath(env.ContentRootPath);
                    config.AddInMemoryCollection(new[]
                           {
                             new KeyValuePair<string,string>("the-key", "the-value")
                           })
                           .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
                           .AddJsonFile($"appsettings.{env}.json", optional: true)
                           .AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddDebug();
                    logging.AddConsole();
                    logging.AddDbLogger(); // You can change its Log Level using the `appsettings.json` file -> Logging -> LogLevel -> Default
                })
                .UseIISIntegration()
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .UseStartup<Startup>();
        }
    }
}
