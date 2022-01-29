using System.ComponentModel;
using ASPNETCoreIdentitySample.Services.Identity;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Areas.Test.Controllers;

/// <summary>
///     More info: http://www.dntips.ir/post/2581
/// </summary>
[Authorize(Policy = ConstantPolicies.DynamicPermission), Area(TestAreaConstants.TestArea),
 BreadCrumb(UseDefaultRouteUrl = true, Order = 0),
 DisplayName("کنترلر نمونه با سطح دسترسی پویا در يك ناحيه خاص آزمايشي")]
public class DynamicPermissionsAreaSampleController : Controller
{
    [DisplayName("ایندکس"), BreadCrumb(Order = 1)]
    public IActionResult Index()
    {
        return View();
    }
}