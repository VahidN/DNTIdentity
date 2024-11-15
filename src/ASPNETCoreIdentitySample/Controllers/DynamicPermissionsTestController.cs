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
[Authorize(Policy = ConstantPolicies.DynamicPermission)]
[BreadCrumb(UseDefaultRouteUrl = true, Order = 0)]
[DisplayName(displayName: "کنترلر آزمایشی با سطح دسترسی پویا")]
public class DynamicPermissionsTestController : Controller
{
    [DisplayName(displayName: "ایندکس")]
    [BreadCrumb(Order = 1)]
    public IActionResult Index() => View();

    [HttpPost] // More info: http://www.dntips.ir/post/2468/ and http://www.dntips.ir/post/2470/
    public IActionResult Index([FromBody] RoleViewModel model) => Json(model);

    [DisplayName(displayName: "گزارش از لیست محصولات")]
    [BreadCrumb(Order = 1)]
    public IActionResult Products() => View(viewName: "Index");

    [DisplayName(displayName: "گزارش از لیست سفارشات")]
    [BreadCrumb(Order = 1)]
    public IActionResult Orders() => View(viewName: "Index");

    [DisplayName(displayName: "گزارش از لیست فروش")]
    [BreadCrumb(Order = 1)]
    public IActionResult Sells() => View(viewName: "Index");

    [DisplayName(displayName: "گزارش از لیست خریداران")]
    [BreadCrumb(Order = 1)]
    public IActionResult Customers() => View(viewName: "Index");
}