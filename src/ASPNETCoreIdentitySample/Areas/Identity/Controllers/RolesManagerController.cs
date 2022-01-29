using ASPNETCoreIdentitySample.Common.IdentityToolkit;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

[Authorize(Roles = ConstantRoles.Admin), Area(AreaConstants.IdentityArea),
 BreadCrumb(Title = "مدیریت نقش‌ها", UseDefaultRouteUrl = true, Order = 0)]
public class RolesManagerController : Controller
{
    private const string RoleNotFound = "نقش درخواستی یافت نشد.";
    private const int DefaultPageSize = 7;

    private readonly IApplicationRoleManager _roleManager;

    public RolesManagerController(IApplicationRoleManager roleManager)
    {
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public IActionResult Index()
    {
        var roles = _roleManager.GetAllCustomRolesAndUsersCountList();
        return View(roles);
    }

    [AjaxOnly]
    public async Task<IActionResult> RenderRole([FromBody] ModelIdViewModel model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (model.Id == 0)
        {
            return PartialView("_Create", new RoleViewModel());
        }

        var role = await _roleManager.FindByIdAsync(model.Id.ToString(CultureInfo.InvariantCulture));
        if (role == null)
        {
            ModelState.AddModelError("", RoleNotFound);
            return PartialView("_Create", new RoleViewModel());
        }

        return PartialView("_Create",
            new RoleViewModel { Id = role.Id.ToString(CultureInfo.InvariantCulture), Name = role.Name });
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(RoleViewModel model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ModelState.AddModelError("", RoleNotFound);
            }
            else
            {
                role.Name = model.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }

                ModelState.AddErrorsFromResult(result);
            }
        }

        return PartialView("_Create", model);
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddRole(RoleViewModel model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var result = await _roleManager.CreateAsync(new Role(model.Name));
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }

            ModelState.AddErrorsFromResult(result);
        }

        return PartialView("_Create", model);
    }

    [AjaxOnly]
    public async Task<IActionResult> RenderDeleteRole([FromBody] ModelIdViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (model == null)
        {
            return BadRequest("model is null.");
        }

        var role = await _roleManager.FindByIdAsync(model.Id.ToString(CultureInfo.InvariantCulture));
        if (role == null)
        {
            ModelState.AddModelError("", RoleNotFound);
            return PartialView("_Delete", new RoleViewModel());
        }

        return PartialView("_Delete",
            new RoleViewModel { Id = role.Id.ToString(CultureInfo.InvariantCulture), Name = role.Name });
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(RoleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (string.IsNullOrWhiteSpace(model?.Id))
        {
            return BadRequest("model is null.");
        }

        var role = await _roleManager.FindByIdAsync(model.Id);
        if (role == null)
        {
            ModelState.AddModelError("", RoleNotFound);
        }
        else
        {
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }

            ModelState.AddErrorsFromResult(result);
        }

        return PartialView("_Delete", model);
    }

    [BreadCrumb(Title = "لیست کاربران دارای نقش", Order = 1)]
    public async Task<IActionResult> UsersInRole(int? id, int? page = 1, string field = "Id",
        SortOrder order = SortOrder.Ascending)
    {
        if (id == null)
        {
            return View("Error");
        }

        var model = await _roleManager.GetPagedApplicationUsersInRoleListAsync(
            id.Value,
            page.Value - 1,
            DefaultPageSize,
            field,
            order,
            true);

        model.Paging.CurrentPage = page.Value;
        model.Paging.ItemsPerPage = DefaultPageSize;
        model.Paging.ShowFirstLast = true;

        if (HttpContext.Request.IsAjaxRequest())
        {
            return PartialView("~/Areas/Identity/Views/UsersManager/_UsersList.cshtml", model);
        }

        return View(model);
    }
}