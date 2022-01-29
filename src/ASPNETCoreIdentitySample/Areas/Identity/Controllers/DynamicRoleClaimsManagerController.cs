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
[Authorize(Roles = ConstantRoles.Admin), Area(AreaConstants.IdentityArea),
 BreadCrumb(Title = "مدیریت نقش‌های پویا", UseDefaultRouteUrl = true, Order = 0)]
public class DynamicRoleClaimsManagerController : Controller
{
    private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;
    private readonly IApplicationRoleManager _roleManager;

    public DynamicRoleClaimsManagerController(
        IMvcActionsDiscoveryService mvcActionsDiscoveryService,
        IApplicationRoleManager roleManager)
    {
        _mvcActionsDiscoveryService = mvcActionsDiscoveryService ??
                                      throw new ArgumentNullException(nameof(mvcActionsDiscoveryService));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public async Task<IActionResult> Index(int? id)
    {
        this.AddBreadCrumb(new BreadCrumb
        {
            Title = "مدیریت نقش‌ها",
            Url = Url.Action("Index", "RolesManager"),
            Order = -1
        });

        if (!id.HasValue)
        {
            return View("Error");
        }

        var role = await _roleManager.FindRoleIncludeRoleClaimsAsync(id.Value);
        if (role == null)
        {
            return View("NotFound");
        }

        var securedControllerActions =
            _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
        return View(new DynamicRoleClaimsManagerViewModel
        {
            SecuredControllerActions = securedControllerActions,
            RoleIncludeRoleClaims = role
        });
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Index(DynamicRoleClaimsManagerViewModel model)
    {
        if (model is null)
        {
            return View("Error");
        }

        var result = await _roleManager.AddOrUpdateRoleClaimsAsync(
            model.RoleId,
            ConstantPolicies.DynamicPermissionClaimType,
            model.ActionIds);
        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(true));
        }

        return Json(new { success = true });
    }
}