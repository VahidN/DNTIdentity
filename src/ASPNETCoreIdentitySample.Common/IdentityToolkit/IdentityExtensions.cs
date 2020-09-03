using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASPNETCoreIdentitySample.Common.IdentityToolkit
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2580
    /// And http://www.dotnettips.info/post/2579
    /// </summary>
    public static class IdentityExtensions
    {
        public static void AddErrorsFromResult(this ModelStateDictionary modelStat, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                modelStat.AddModelError("", error.Description);
            }
        }

        /// <summary>
        /// IdentityResult errors list to string
        /// </summary>
        public static string DumpErrors(this IdentityResult result, bool useHtmlNewLine = false)
        {
            var results = new StringBuilder();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    var errorDescription = error.Description;
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
}