using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ASPNETCoreIdentitySample.DataLayer.Context;

/// <summary>
///     More info: http://www.dntips.ir/post/2509
/// </summary>
public interface IUnitOfWork : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
    T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible;
    object GetShadowPropertyValue(object entity, string propertyName);

    void ExecuteSqlInterpolatedCommand(FormattableString query);
    void ExecuteSqlRawCommand(string query, params object[] parameters);

    void BeginTransaction();
    void RollbackTransaction();
    void CommitTransaction();

    int SaveChanges(bool acceptAllChangesOnSuccess);
    int SaveChanges();
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new());
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}