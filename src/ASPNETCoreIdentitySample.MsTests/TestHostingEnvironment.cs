using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.FileProviders;

namespace ASPNETCoreIdentitySample.MsTests
{
    public class TestHostingEnvironment : IWebHostEnvironment
    {
        public TestHostingEnvironment()
        {
            ApplicationName = typeof(TestHostingEnvironment).Assembly.FullName;
            WebRootFileProvider = new NullFileProvider();
            ContentRootFileProvider = new NullFileProvider();
            ContentRootPath = AppContext.BaseDirectory;
            WebRootPath = AppContext.BaseDirectory;
        }

        public string EnvironmentName { get; set; }
        public string ApplicationName { get; set; }
        public string WebRootPath { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
    }
}