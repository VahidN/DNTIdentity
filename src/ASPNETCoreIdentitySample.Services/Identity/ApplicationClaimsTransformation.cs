using System.Security.Claims;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     To register it: services.AddScoped(IClaimsTransformation, ApplicationClaimsTransformation)();
///     How to add existing db user's claims to the user's active directory claims.
///     More info: http://www.dntips.ir/post/2762
/// </summary>
public class ApplicationClaimsTransformation(
    IApplicationUserManager userManager,
    IApplicationRoleManager roleManager,
    ILogger<ApplicationClaimsTransformation> logger) : IClaimsTransformation
{
    private readonly ILogger<ApplicationClaimsTransformation> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IApplicationRoleManager _roleManager =
        roleManager ?? throw new ArgumentNullException(nameof(roleManager));

    private readonly IApplicationUserManager _userManager =
        userManager ?? throw new ArgumentNullException(nameof(userManager));

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        if (principal.Identity is not ClaimsIdentity identity || !IsNtlm(identity))
        {
            return principal;
        }

        var claims = await AddExistingUserClaimsAsync(identity);
        identity.AddClaims(claims);

        return principal;
    }

    private async Task<IEnumerable<Claim>> AddExistingUserClaimsAsync(ClaimsIdentity identity)
    {
        var claims = new List<Claim>();

        var user = await _userManager.Users.Include(u => u.Claims)
            .FirstOrDefaultAsync(u => u.UserName == identity.Name);

        if (user == null)
        {
            _logger.LogErrorMessage($"Couldn't find {identity.Name}.");

            return claims;
        }

        var options = new ClaimsIdentityOptions();

        claims.Add(new Claim(options.UserIdClaimType, user.Id.ToString(CultureInfo.InvariantCulture)));
        claims.Add(new Claim(options.UserNameClaimType, user.UserName));

        if (_userManager.SupportsUserSecurityStamp)
        {
            claims.Add(new Claim(options.SecurityStampClaimType, await _userManager.GetSecurityStampAsync(user)));
        }

        if (_userManager.SupportsUserClaim)
        {
            claims.AddRange(await _userManager.GetClaimsAsync(user));
        }

        if (_userManager.SupportsUserRole)
        {
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var roleName in roles)
            {
                claims.Add(new Claim(options.RoleClaimType, roleName));

                if (IsNtlm(identity))
                {
                    claims.Add(new Claim(ClaimTypes.GroupSid, roleName));
                }

                if (_roleManager.SupportsRoleClaims)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);

                    if (role != null)
                    {
                        claims.AddRange(await _roleManager.GetClaimsAsync(role));
                    }
                }
            }
        }

        return claims;
    }

    private static bool IsNtlm(ClaimsIdentity identity)
        => string.Equals(identity.AuthenticationType, b: "NTLM", StringComparison.OrdinalIgnoreCase);
}