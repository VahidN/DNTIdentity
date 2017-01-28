using System;

namespace ASPNETCoreIdentitySample.Entities.Identity
{
    /// <summary>
    /// For Microsoft.Extensions.Caching.SqlServer
    /// </summary>
    public class AppSqlCache
    {
        public string Id { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset ExpiresAtTime { get; set; }
        public long? SlidingExpirationInSeconds { get; set; }
        public DateTimeOffset? AbsoluteExpiration { get; set; }
    }
}