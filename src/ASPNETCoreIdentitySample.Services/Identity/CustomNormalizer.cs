using ASPNETCoreIdentitySample.Common.PersianToolkit;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Identity;

namespace ASPNETCoreIdentitySample.Services.Identity;

/// <summary>
///     More info: http://www.dntips.ir/post/2579
/// </summary>
public class CustomNormalizer : ILookupNormalizer
{
    public string NormalizeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }

        email = email.Trim();
        email = FixGmailDots(email);
        email = email.ToUpperInvariant();
        return email;
    }

    public string NormalizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        name = name.Trim();
        name = name.ApplyCorrectYeKe()
            .RemoveDiacritics()
            .CleanUnderLines()
            .RemovePunctuation();
        name = name.Trim().Replace(" ", "", StringComparison.OrdinalIgnoreCase);
        name = name.ToUpperInvariant();
        return name;
    }

    private static string FixGmailDots(string email)
    {
        email = email.ToLowerInvariant().Trim();
        var emailParts = email.Split('@');
        var name = emailParts[0].Replace(".", string.Empty, StringComparison.OrdinalIgnoreCase);

        var plusIndex = name.IndexOf('+', StringComparison.OrdinalIgnoreCase);
        if (plusIndex != -1)
        {
            name = name.Substring(0, plusIndex);
        }

        var emailDomain = emailParts[1];
        emailDomain = emailDomain.Replace("googlemail.com", "gmail.com", StringComparison.OrdinalIgnoreCase);

        string[] domainsAllowedDots =
        {
            "gmail.com",
            "facebook.com"
        };

        var isFromDomainsAllowedDots =
            domainsAllowedDots.Any(domain => emailDomain.Equals(domain, StringComparison.OrdinalIgnoreCase));
        return !isFromDomainsAllowedDots
            ? email
            : string.Format(CultureInfo.InvariantCulture, "{0}@{1}", name, emailDomain);
    }
}