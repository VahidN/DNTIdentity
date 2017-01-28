using ASPNETCoreIdentitySample.Services.Identity;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ASPNETCoreIdentitySample.Controllers
{
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [BreadCrumb(UseDefaultRouteUrl = true, Order = 0)]
    [DisplayName("کنترلر آزمایشی با سطح دسترسی پویا")]
    public class DynamicPermissionsTestController : Controller
    {
        [DisplayName("ایندکس")]
        [BreadCrumb(Order = 1)]
        public IActionResult Index()
        {
            return View();
        }

        [DisplayName("گزارش از لیست محصولات")]
        [BreadCrumb(Order = 1)]
        public IActionResult Products()
        {
            return View(viewName: "Index");
        }

        [DisplayName("گزارش از لیست سفارشات")]
        [BreadCrumb(Order = 1)]
        public IActionResult Orders()
        {
            return View(viewName: "Index");
        }

        [DisplayName("گزارش از لیست فروش")]
        [BreadCrumb(Order = 1)]
        public IActionResult Sells()
        {
            return View(viewName: "Index");
        }

        [DisplayName("گزارش از لیست خریداران")]
        [BreadCrumb(Order = 1)]
        public IActionResult Customers()
        {
            return View(viewName: "Index");
        }
    }
}