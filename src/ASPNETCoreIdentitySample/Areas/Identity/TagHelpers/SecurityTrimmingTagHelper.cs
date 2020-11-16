using System;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ASPNETCoreIdentitySample.Areas.Identity.TagHelpers
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2527
    /// And http://www.dotnettips.info/post/2581
    /// </summary>
    [HtmlTargetElement("security-trimming")]
    public class SecurityTrimmingTagHelper : TagHelper
    {
        private readonly ISecurityTrimmingService _securityTrimmingService;

        public SecurityTrimmingTagHelper(ISecurityTrimmingService securityTrimmingService)
        {
            _securityTrimmingService = securityTrimmingService ?? throw new ArgumentNullException(nameof(securityTrimmingService));
        }

        /// <summary>
        /// The name of the action method.
        /// </summary>
        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        /// <summary>
        /// The name of the area.
        /// </summary>
        [HtmlAttributeName("asp-area")]
        public string Area { get; set; }

        /// <summary>
        /// The name of the controller.
        /// </summary>
        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            // don't render the <security-trimming> tag.
            output.TagName = null;

            if (!ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // suppress the output and generate nothing.
                output.SuppressOutput();
            }

            if (_securityTrimmingService.CanCurrentUserAccess(Area, Controller, Action))
            {
                // fine, do nothing.
                return;
            }

            // else, suppress the output and generate nothing.
            output.SuppressOutput();
        }
    }
}