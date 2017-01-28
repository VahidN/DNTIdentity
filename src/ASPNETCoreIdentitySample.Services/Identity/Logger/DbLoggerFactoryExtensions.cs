using System;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreIdentitySample.Services.Identity.Logger
{
    public static class DbLoggerFactoryExtensions
    {
        public static ILoggerFactory AddDbLogger(
                    this ILoggerFactory factory,
                    IServiceProvider serviceProvider,
                    LogLevel minLevel)
        {
            Func<string, LogLevel, bool> logFilter = delegate (string loggerName, LogLevel logLevel)
            {
                return logLevel >= minLevel;
            };

            factory.AddProvider(new DbLoggerProvider(logFilter, serviceProvider));
            return factory;
        }
    }
}