using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/3407
/// </summary>
public class PasswordRules : IPasswordRules
{
    private readonly IdentityOptions _options;

    public PasswordRules(
        IOptions<IdentityOptions> optionsAccessor) =>
        _options = optionsAccessor?.Value;

    public string GetPasswordRules()
    {
        var options = _options.Password;

        var passwordRules = new StringBuilder();
        passwordRules.AppendFormat(CultureInfo.InvariantCulture, "minlength: {0};", options.RequiredLength);

        if (options.RequireLowercase)
        {
            passwordRules.Append(" required: lower;");
        }

        if (options.RequireUppercase)
        {
            passwordRules.Append(" required: upper;");
        }

        if (options.RequireDigit)
        {
            passwordRules.Append(" required: digit;");
        }

        if (options.RequireNonAlphanumeric)
        {
            passwordRules.Append(" required: special;");
        }

        return passwordRules.ToString();
    }
}