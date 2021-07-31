using System.ComponentModel;
using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2581
    /// </summary>
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [Area(AreaConstants.IdentityArea)]
    [BreadCrumb(UseDefaultRouteUrl = true, Order = 0)]
    [DisplayName("کنترلر نمونه با سطح دسترسی پویا در يك ناحيه خاص")]
    public class DynamicPermissionsAreaSampleController : Controller
    {
        [DisplayName("ایندکس")]
        [BreadCrumb(Order = 1)]
        public IActionResult Index()
        {
            return View();
        }
    }
}