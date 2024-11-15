using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

[AllowAnonymous]
[Area(AreaConstants.IdentityArea)]
[BreadCrumb(Title = "برگه‌ی کاربری", UseDefaultRouteUrl = true, Order = 0)]
public class UserCardController(IApplicationUserManager userManager, IApplicationRoleManager roleManager) : Controller
{
    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public async Task<IActionResult> Index(int? id)
    {
        if (!id.HasValue && User.Identity is { IsAuthenticated: true })
        {
            id = User.Identity.GetUserId<int>();
        }

        if (!id.HasValue)
        {
            return View(viewName: "Error");
        }

        var user = await userManager.FindByIdIncludeUserRolesAsync(id.Value);

        if (user == null)
        {
            return View(viewName: "NotFound");
        }

        var model = new UserCardItemViewModel
        {
            User = user,
            ShowAdminParts = User.IsInRole(ConstantRoles.Admin),
            Roles = await roleManager.GetAllCustomRolesAsync(),
            ActiveTab = UserCardItemActiveTab.UserInfo
        };

        return View(model);
    }

    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> EmailToImage(int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var fileContents = await userManager.GetEmailImageAsync(id);

        return new FileContentResult(fileContents, contentType: "image/png");
    }

    [BreadCrumb(Title = "لیست کاربران آنلاین", Order = 1)]
    public IActionResult OnlineUsers() => View();
}