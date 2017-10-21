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

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Task.FromResult(principal);
            }

            var claims = addExistingUserClaims(identity);
            identity.AddClaims(claims);

            return Task.FromResult(principal);
        }

        private IEnumerable<Claim> addExistingUserClaims(IIdentity identity)
        {
            var claims = new List<Claim>();
            var existingUserClaims = _userManager.Users.Include(u => u.Claims)
                                                 .SingleOrDefault(u => u.UserName == identity.Name);
            if (existingUserClaims == null)
            {
                return claims;
            }

            claims.AddRange(existingUserClaims.Claims.Select(c => c.ToClaim()));
            return claims;
        }
    }
}