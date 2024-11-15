using System.Security.Claims;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     Customizing claims transformation in ASP.NET Core Identity
///     More info: http://www.dntips.ir/post/2580
/// </summary>
public class ApplicationClaimsPrincipalFactory(
    IApplicationUserManager userManager,
    IApplicationRoleManager roleManager,
    IOptions<IdentityOptions> optionsAccessor) : UserClaimsPrincipalFactory<User, Role>((UserManager<User>)userManager,
    (RoleManager<Role>)roleManager, optionsAccessor)
{
    public static readonly string PhotoFileName = nameof(PhotoFileName);

    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var
            principal = await base
                .CreateAsync(
                    user); // adds all `Options.ClaimsIdentity.RoleClaimType -> Role Claims` automatically + `Options.ClaimsIdentity.UserIdClaimType -> userId` & `Options.ClaimsIdentity.UserNameClaimType -> userName`

        AddCustomClaims(user, principal);

        return principal;
    }

    private static void AddCustomClaims(User user, ClaimsPrincipal principal)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        ((ClaimsIdentity)principal.Identity)?.AddClaims([
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture),
                ClaimValueTypes.Integer),
            new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
            new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
            new Claim(PhotoFileName, user.PhotoFileName ?? string.Empty, ClaimValueTypes.String)
        ]);
    }
}