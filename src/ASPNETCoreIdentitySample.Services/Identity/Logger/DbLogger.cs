using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace ASPNETCoreIdentitySample.Services.Identity.Logger
{
    public class DbLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly IServiceProvider _serviceProvider;
        private readonly DbLoggerProvider _loggerProvider;
        private readonly IOptions<SiteSettings> _siteSettings;
        private readonly LogLevel _minLevel;

        public DbLogger(
            DbLoggerProvider loggerProvider,
            IServiceProvider serviceProvider,
            string loggerName,
            IOptions<SiteSettings> siteSettings)
        {
            _loggerName = loggerName;

            _siteSettings = siteSettings;
            _siteSettings.CheckArgumentIsNull(nameof(_siteSettings));
            _minLevel = _siteSettings.Value.Logging.LogLevel.Default;

            _serviceProvider = serviceProvider;
            _serviceProvider.CheckArgumentIsNull(nameof(_serviceProvider));

            _loggerProvider = loggerProvider;
            _loggerProvider.CheckArgumentIsNull(nameof(_loggerProvider));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new NoopDisposable();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _minLevel;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (exception != null)
            {
                message = $"{message}{Environment.NewLine}{exception}";
            }

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var appLogItem = new AppLogItem
            {
                Url = httpContextAccessor?.HttpContext != null ? httpContextAccessor.HttpContext.Request.Path.ToString() : string.Empty,
                EventId = eventId.Id,
                LogLevel = logLevel.ToString(),
                Logger = _loggerName,
                Message = message
            };
            setStateJson(state, appLogItem);
            _loggerProvider.AddLogItem(appLogItem);
        }

        private static void setStateJson<TState>(TState state, AppLogItem appLogItem)
        {
            try
            {
                appLogItem.StateJson = JsonConvert.SerializeObject(
                    state,
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Include
                    });
            }
            catch
            {
                // don't throw exceptions from logger
            }
        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}