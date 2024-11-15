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
[DisplayName(displayName: "کنترلر نمونه با سطح دسترسی پویا")]
public class DynamicPermissionsSampleController : Controller
{
    [DisplayName(displayName: "ایندکس")]
    [BreadCrumb(Order = 1)]
    public IActionResult Index() => View();

    [HttpPost] [ValidateAntiForgeryToken] public IActionResult Index(RoleViewModel model) => View(model);

    [DisplayName(displayName: "گزارش از لیست کتاب‌ها")]
    [BreadCrumb(Order = 1)]
    public IActionResult Books() => View(viewName: "Index");

    [DisplayName(displayName: "گزارش از لیست مراجعان")]
    [BreadCrumb(Order = 1)]
    public IActionResult Users() => View(viewName: "Index");

    [DisplayName(displayName: "گزارش از لیست امانات")]
    [BreadCrumb(Order = 1)]
    public IActionResult BooksGiven() => View(viewName: "Index");

    [DisplayName(displayName: "گزارش از لیست مفقودی‌ها")]
    [BreadCrumb(Order = 1)]
    public IActionResult BooksMissings() => View(viewName: "Index");
}