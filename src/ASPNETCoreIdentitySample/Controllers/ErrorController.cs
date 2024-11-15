using System.Text;
using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Controllers;

[BreadCrumb(Title = "خطا", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "fas fa-warning")]
public class ErrorController(ILogger<ErrorController> logger) : Controller
{
    /// <summary>
    ///     More info: http://www.dntips.ir/post/2446
    /// </summary>
    [BreadCrumb(Title = "ایندکس", Order = 2, GlyphIcon = "fas fa-navicon")]
    public IActionResult Index(int? id)
    {
        var logBuilder = new StringBuilder();

        var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

        logBuilder.Append(value: "Error ")
            .Append(id)
            .Append(value: " for ")
            .Append(Request.Method)
            .Append(value: ' ')
            .Append(statusCodeReExecuteFeature?.OriginalPath ?? Request.Path.Value)
            .Append(Request.QueryString.Value)
            .AppendLine(value: "\n");

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

        if (exceptionHandlerFeature?.Error != null)
        {
            var exception = exceptionHandlerFeature.Error;

            logBuilder.Append(value: "<h1>Exception: ")
                .Append(exception.Message)
                .Append(value: "</h1>")
                .AppendLine(exception.StackTrace);
        }

        foreach (var header in Request.Headers)
        {
            var headerValues = header.Value.ToString();
            logBuilder.Append(header.Key).Append(value: ": ").AppendLine(headerValues);
        }

        logger.LogErrorMessage(logBuilder.ToString());

        if (id == null)
        {
            return View(viewName: "Error");
        }

        return id.Value switch
        {
            401 or 403 => View(viewName: "AccessDenied"),
            404 => View(viewName: "NotFound"),
            _ => View(viewName: "Error")
        };
    }
}