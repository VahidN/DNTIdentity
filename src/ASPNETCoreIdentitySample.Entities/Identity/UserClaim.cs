using ASPNETCoreIdentitySample.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace ASPNETCoreIdentitySample.Entities.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/2577
///     and http://www.dntips.ir/post/2578
/// </summary>
public class UserClaim : IdentityUserClaim<int>, IAuditableEntity
{
    public virtual User User { get; set; }
}