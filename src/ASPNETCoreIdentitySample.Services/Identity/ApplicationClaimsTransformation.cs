using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreIdentitySample.Services.Identity
{
    /// <summary>
    ///  To register it: services.AddScoped<IClaimsTransformation, ApplicationClaimsTransformation>();
    ///  How to add existing db user's claims to the user's active directory claims.
    /// More info: http://www.dotnettips.info/post/2762
    /// </summary>
    public class ApplicationClaimsTransformation : IClaimsTransformation
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly ILogger<ApplicationClaimsTransformation> _logger;

        public ApplicationClaimsTransformation(
            IApplicationUserManager userManager,
            IApplicationRoleManager roleManager,
            ILogger<ApplicationClaimsTransformation> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (!(principal.Identity is ClaimsIdentity identity) || !isNTLM(identity))
            {
                return principal;
            }

            var claims = await addExistingUserClaimsAsync(identity);
            identity.AddClaims(claims);

            return principal;
        }

        private async Task<IEnumerable<Claim>> addExistingUserClaimsAsync(IIdentity identity)
        {
            var claims = new List<Claim>();
            var user = await _userManager.Users.Include(u => u.Claims)
                                                 .FirstOrDefaultAsync(u => u.UserName == identity.Name)
                                                 ;
            if (user == null)
            {
                _logger.LogError($"Couldn't find {identity.Name}.");
                return claims;
            }

            var Options = new ClaimsIdentityOptions();

            claims.Add(new Claim(Options.UserIdClaimType, user.Id.ToString()));
            claims.Add(new Claim(Options.UserNameClaimType, user.UserName));

            if (_userManager.SupportsUserSecurityStamp)
            {
                claims.Add(new Claim(Options.SecurityStampClaimType,
                    await _userManager.GetSecurityStampAsync(user)));
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
                    claims.Add(new Claim(Options.RoleClaimType, roleName));

                    if (isNTLM(identity))
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

        private static bool isNTLM(IIdentity identity)
        {
            return identity.AuthenticationType == "NTLM";
        }
    }
}