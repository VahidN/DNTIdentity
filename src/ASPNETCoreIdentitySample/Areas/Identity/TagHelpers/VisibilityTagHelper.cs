using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ASPNETCoreIdentitySample.Areas.Identity.TagHelpers
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2527
    /// And http://www.dotnettips.info/post/2581
    /// </summary>
    [HtmlTargetElement("div")]
    public class VisibilityTagHelper : TagHelper
    {
        /// <summary>
        /// default to true otherwise all existing target elements will not be shown, because bool's default to false
        /// </summary>
        [HtmlAttributeName("asp-is-visible")]
        public bool IsVisible { get; set; } = true;

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            if (!IsVisible)
            {
                // suppress the output and generate nothing.
                output.SuppressOutput();
            }

            return base.ProcessAsync(context, output);
        }
    }
}