using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace ASPNETCoreIdentitySample.MsTests;

public class TestHostingEnvironment : IWebHostEnvironment
{
    public string EnvironmentName { get; set; }

    public string ApplicationName { get; set; } = typeof(TestHostingEnvironment).Assembly.FullName;

    public string WebRootPath { get; set; } = AppContext.BaseDirectory;

    public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();

    public string ContentRootPath { get; set; } = AppContext.BaseDirectory;

    public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
}