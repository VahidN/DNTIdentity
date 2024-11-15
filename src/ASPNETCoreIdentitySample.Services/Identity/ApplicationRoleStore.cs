using System.Security.Claims;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/2578
/// </summary>
public class ApplicationRoleStore(IUnitOfWork uow, IdentityErrorDescriber describer)
    : RoleStore<Role, ApplicationDbContext, int, UserRole, RoleClaim>((ApplicationDbContext)uow, describer),
        IApplicationRoleStore
{
    protected override RoleClaim CreateRoleClaim(Role role, Claim claim)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        if (claim == null)
        {
            throw new ArgumentNullException(nameof(claim));
        }

        return new RoleClaim
        {
            RoleId = role.Id,
            ClaimType = claim.Type,
            ClaimValue = claim.Value
        };
    }
}