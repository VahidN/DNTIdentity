using System.Threading.Tasks;
using ASPNETCoreIdentitySample.Common.GuardToolkit;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ASPNETCoreIdentitySample.Areas.Identity.TagHelpers
{
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
            context.CheckArgumentIsNull(nameof(context));
            output.CheckArgumentIsNull(nameof(output));

            if (!IsVisible)
            {
                // suppress the output and generate nothing.
                output.SuppressOutput();
            }

            return base.ProcessAsync(context, output);
        }
    }
}