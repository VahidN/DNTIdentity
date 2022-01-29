using System.Collections.Concurrent;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using DNTCommon.Web.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity.Logger;

public class DbLoggerProvider : ILoggerProvider
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly IList<LoggerItem> _currentBatch = new List<LoggerItem>();
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(2);

    private readonly BlockingCollection<LoggerItem> _messageQueue = new(new ConcurrentQueue<LoggerItem>());

    private readonly Task _outputTask;
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<SiteSettings> _siteSettings;
    private bool _isDisposed;

    public DbLoggerProvider(
        IOptions<SiteSettings> siteSettings,
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _siteSettings = siteSettings ?? throw new ArgumentNullException(nameof(siteSettings));
        _outputTask = Task.Run(ProcessLogQueue);
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DbLogger(this, _serviceProvider, categoryName, _siteSettings);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            try
            {
                if (disposing)
                {
                    Stop();
                    _messageQueue.Dispose();
                    _cancellationTokenSource.Dispose();
                }
            }
            finally
            {
                _isDisposed = true;
            }
        }
    }

    internal void AddLogItem(LoggerItem appLogItem)
    {
        if (!_messageQueue.IsAddingCompleted)
        {
            _messageQueue.Add(appLogItem, _cancellationTokenSource.Token);
        }
    }

    private async Task ProcessLogQueue()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            while (_messageQueue.TryTake(out var message))
            {
                try
                {
                    _currentBatch.Add(message);
                }
                catch
                {
                    //cancellation token canceled or CompleteAdding called
                }
            }

            await SaveLogItemsAsync(_currentBatch, _cancellationTokenSource.Token);
            _currentBatch.Clear();

            await Task.Delay(_interval, _cancellationTokenSource.Token);
        }
    }

    private async Task SaveLogItemsAsync(IList<LoggerItem> items, CancellationToken cancellationToken)
    {
        try
        {
            if (!items.Any())
            {
                return;
            }

            // We need a separate context for the logger to call its SaveChanges several times,
            // without using the current request's context and changing its internal state.
            await _serviceProvider.RunScopedServiceAsync<IUnitOfWork>(async context =>
            {
                foreach (var item in items)
                {
                    var addedEntry = context.Set<AppLogItem>().Add(item.AppLogItem);
                    addedEntry.SetAddedShadowProperties(item.Props);
                }

                await context.SaveChangesAsync(cancellationToken);
            });
        }
        catch
        {
            // don't throw exceptions from logger
        }
    }

    [SuppressMessage("Microsoft.Usage", "CA1031:catch a more specific allowed exception type, or rethrow the exception",
        Justification = "don't throw exceptions from logger")]
    private void Stop()
    {
        _cancellationTokenSource.Cancel();
        _messageQueue.CompleteAdding();

        try
        {
            _outputTask.Wait(_interval);
        }
        catch
        {
            // don't throw exceptions from logger
        }
    }
}