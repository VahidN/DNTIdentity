using System.ComponentModel;
using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Controllers;

/// <summary>
///     More info: http://www.dntips.ir/post/2581
/// </summary>
[Authorize(Policy = ConstantPolicies.DynamicPermission), BreadCrumb(UseDefaultRouteUrl = true, Order = 0),
 DisplayName("کنترلر آزمایشی با سطح دسترسی پویا")]
// [NoBrowserCache]
public class DynamicPermissionsTestController : Controller
{
    [DisplayName("ایندکس"), BreadCrumb(Order = 1)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost] // More info: http://www.dntips.ir/post/2468/ and http://www.dntips.ir/post/2470/
    public IActionResult Index([FromBody] RoleViewModel model)
    {
        return Json(model);
    }

    [DisplayName("گزارش از لیست محصولات"), BreadCrumb(Order = 1)]
    public IActionResult Products()
    {
        return View("Index");
    }

    [DisplayName("گزارش از لیست سفارشات"), BreadCrumb(Order = 1)]
    public IActionResult Orders()
    {
        return View("Index");
    }

    [DisplayName("گزارش از لیست فروش"), BreadCrumb(Order = 1)]
    public IActionResult Sells()
    {
        return View("Index");
    }

    [DisplayName("گزارش از لیست خریداران"), BreadCrumb(Order = 1)]
    public IActionResult Customers()
    {
        return View("Index");
    }
}