using ASPNETCoreIdentitySample.Common.IdentityToolkit;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

/// <summary>
///     More info: http://www.dntips.ir/post/2581
/// </summary>
[Authorize(Roles = ConstantRoles.Admin)]
[Area(AreaConstants.IdentityArea)]
[BreadCrumb(Title = "مدیریت نقش‌های پویا", UseDefaultRouteUrl = true, Order = 0)]
public class DynamicRoleClaimsManagerController(
    IMvcActionsDiscoveryService mvcActionsDiscoveryService,
    IApplicationRoleManager roleManager) : Controller
{
    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public async Task<IActionResult> Index(int? id)
    {
        this.AddBreadCrumb(new BreadCrumb
        {
            Title = "مدیریت نقش‌ها",
            Url = Url.Action(action: "Index", controller: "RolesManager"),
            Order = -1
        });

        if (!id.HasValue)
        {
            return View(viewName: "Error");
        }

        var role = await roleManager.FindRoleIncludeRoleClaimsAsync(id.Value);

        if (role == null)
        {
            return View(viewName: "NotFound");
        }

        var securedControllerActions =
            mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);

        return View(new DynamicRoleClaimsManagerViewModel
        {
            SecuredControllerActions = securedControllerActions,
            RoleIncludeRoleClaims = role
        });
    }

    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Index(DynamicRoleClaimsManagerViewModel model)
    {
        if (model is null)
        {
            return View(viewName: "Error");
        }

        var result = await roleManager.AddOrUpdateRoleClaimsAsync(model.RoleId,
            ConstantPolicies.DynamicPermissionClaimType, model.ActionIds);

        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(useHtmlNewLine: true));
        }

        return Json(new
        {
            success = true
        });
    }
}