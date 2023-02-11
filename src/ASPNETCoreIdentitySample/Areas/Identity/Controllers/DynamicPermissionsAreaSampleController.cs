using System.ComponentModel;
using ASPNETCoreIdentitySample.Services.Identity;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

/// <summary>
///     More info: http://www.dntips.ir/post/2581
/// </summary>
[Authorize(Policy = ConstantPolicies.DynamicPermission), Area(AreaConstants.IdentityArea),
 BreadCrumb(UseDefaultRouteUrl = true, Order = 0), DisplayName("کنترلر نمونه با سطح دسترسی پویا در یک ناحیه خاص")]
public class DynamicPermissionsAreaSampleController : Controller
{
    [DisplayName("ایندکس"), BreadCrumb(Order = 1)]
    public IActionResult Index()
    {
        return View();
    }
}