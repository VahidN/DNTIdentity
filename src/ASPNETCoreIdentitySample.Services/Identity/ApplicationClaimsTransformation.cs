using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreIdentitySample.Services.Identity
{
    /// <summary>
    ///  To register it: services.AddScoped<IClaimsTransformation, ApplicationClaimsTransformation>();
    ///  How to add existing db user's claims to the user's active directory claims.
    /// </summary>
    public class ApplicationClaimsTransformation : IClaimsTransformation
    {
        private readonly IApplicationUserManager _userManager;
        public ApplicationClaimsTransformation(IApplicationUserManager userManager)
        {
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return principal;
            }

            var claims = await addExistingUserClaims(identity).ConfigureAwait(false);
            identity.AddClaims(claims);

            return principal;
        }

        private async Task<IEnumerable<Claim>> addExistingUserClaims(IIdentity identity)
        {
            var claims = new List<Claim>();
            var existingUserClaims = await _userManager.Users.Include(u => u.Claims)
                                                 .FirstOrDefaultAsync(u => u.UserName == identity.Name)
                                                 .ConfigureAwait(false);
            if (existingUserClaims == null)
            {
                return claims;
            }

            claims.AddRange(existingUserClaims.Claims.Select(c => c.ToClaim()));
            return claims;
        }
    }
}