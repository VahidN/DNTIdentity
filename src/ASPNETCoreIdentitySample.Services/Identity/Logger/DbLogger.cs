using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity.Logger;

public class DbLogger : ILogger
{
    private readonly string _loggerName;
    private readonly DbLoggerProvider _loggerProvider;
    private readonly LogLevel _minLevel;
    private readonly IServiceProvider _serviceProvider;

    public DbLogger(
        DbLoggerProvider loggerProvider,
        IServiceProvider serviceProvider,
        string loggerName,
        IOptions<SiteSettings> siteSettings)
    {
        _loggerName = loggerName;
        ArgumentNullException.ThrowIfNull(siteSettings);
        _minLevel = siteSettings.Value.Logging.LogLevel.Default;
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _loggerProvider = loggerProvider ?? throw new ArgumentNullException(nameof(loggerProvider));
    }

    public IDisposable BeginScope<TState>(TState state) => new NoopDisposable();

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLevel;

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

        var httpContextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
        var appLogItem = new AppLogItem
                         {
                             Url = httpContextAccessor?.HttpContext != null
                                       ? httpContextAccessor.HttpContext.Request.Path.ToString()
                                       : string.Empty,
                             EventId = eventId.Id,
                             LogLevel = logLevel.ToString(),
                             Logger = _loggerName,
                             Message = message,
                         };
        var props = httpContextAccessor?.GetShadowProperties();
        SetStateJson(state, appLogItem);
        _loggerProvider.AddLogItem(new LoggerItem { Props = props, AppLogItem = appLogItem });
    }

    private static void SetStateJson<TState>(TState state, AppLogItem appLogItem)
    {
        try
        {
            appLogItem.StateJson = JsonSerializer.Serialize(
                                                            state,
                                                            new JsonSerializerOptions
                                                            {
                                                                DefaultIgnoreCondition =
                                                                    JsonIgnoreCondition.WhenWritingNull,
                                                                WriteIndented = true,
                                                            });
        }
        catch
        {
            // don't throw exceptions from logger
        }
    }

    private sealed class NoopDisposable : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static void Dispose(bool disposing)
        {
            // empty on purpose
        }
    }
}