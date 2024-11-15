using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/3407
/// </summary>
public class PasswordRules(IOptions<IdentityOptions> optionsAccessor) : IPasswordRules
{
    private readonly IdentityOptions _options = optionsAccessor?.Value;

    public string GetPasswordRules()
    {
        var options = _options.Password;

        var passwordRules = new StringBuilder();
        passwordRules.AppendFormat(CultureInfo.InvariantCulture, format: "minlength: {0};", options.RequiredLength);

        if (options.RequireLowercase)
        {
            passwordRules.Append(value: " required: lower;");
        }

        if (options.RequireUppercase)
        {
            passwordRules.Append(value: " required: upper;");
        }

        if (options.RequireDigit)
        {
            passwordRules.Append(value: " required: digit;");
        }

        if (options.RequireNonAlphanumeric)
        {
            passwordRules.Append(value: " required: special;");
        }

        return passwordRules.ToString();
    }
}