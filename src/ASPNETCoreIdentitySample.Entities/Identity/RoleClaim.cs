using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace ASPNETCoreIdentitySample.Entities.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/2577
///     and http://www.dntips.ir/post/2578
/// </summary>
public class RoleClaim : IdentityRoleClaim<int>, IAuditableEntity
{
    public virtual Role Role { get; set; }
}