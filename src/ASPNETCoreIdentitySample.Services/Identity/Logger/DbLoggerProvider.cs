using ASPNETCoreIdentitySample.Common.GuardToolkit;
using Microsoft.Extensions.Logging;
using System;

namespace ASPNETCoreIdentitySample.Services.Identity.Logger
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly IServiceProvider _serviceProvider;

        public DbLoggerProvider(
                    Func<string, LogLevel, bool> filter,
                    IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceProvider.CheckArgumentIsNull(nameof(_serviceProvider));

            _filter = filter;
            _filter.CheckArgumentIsNull(nameof(_filter));
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(_serviceProvider, categoryName, _filter);
        }

        public void Dispose()
        {
        }
    }
}