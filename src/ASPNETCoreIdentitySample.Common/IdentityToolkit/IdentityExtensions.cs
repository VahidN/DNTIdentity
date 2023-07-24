using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASPNETCoreIdentitySample.Common.IdentityToolkit;

/// <summary>
///     More info: http://www.dntips.ir/post/2580
///     And http://www.dntips.ir/post/2579
/// </summary>
public static class IdentityExtensions
{
    public static void AddErrorsFromResult(this ModelStateDictionary modelStat, IdentityResult result)
    {
        if (modelStat == null)
        {
            throw new ArgumentNullException(nameof(modelStat));
        }

        if (result == null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        foreach (var error in result.Errors)
        {
            modelStat.AddModelError("", error.Description);
        }
    }

    /// <summary>
    ///     IdentityResult errors list to string
    /// </summary>
    public static string DumpErrors(this IdentityResult result, bool useHtmlNewLine = false)
    {
        if (result == null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        var results = new StringBuilder();
        if (!result.Succeeded)
        {
            foreach (var errorDescription in result.Errors.Select(x => x.Description).Distinct())
            {
                if (string.IsNullOrWhiteSpace(errorDescription))
                {
                    continue;
                }

                if (!useHtmlNewLine)
                {
                    results.AppendLine(errorDescription);
                }
                else
                {
                    results.Append(errorDescription).AppendLine("<br/>");
                }
            }
        }

        return results.ToString();
    }
}