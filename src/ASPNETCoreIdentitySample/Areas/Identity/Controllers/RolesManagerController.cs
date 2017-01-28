using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.Common.IdentityToolkit;
using ASPNETCoreIdentitySample.Common.WebToolkit;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ASPNETCoreIdentitySample.Services.Identity;
using DNTBreadCrumb.Core;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers
{
    [Authorize(Roles = ConstantRoles.Admin)]
    [Area(AreaConstants.IdentityArea)]
    [BreadCrumb(Title = "مدیریت نقش‌ها", UseDefaultRouteUrl = true, Order = 0)]
    public class RolesManagerController : Controller
    {
        private const string RoleNotFound = "نقش درخواستی یافت نشد.";
        private const int DefaultPageSize = 7;

        private readonly IApplicationRoleManager _roleManager;

        public RolesManagerController(IApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(_roleManager));
        }

        [BreadCrumb(Title = "ایندکس", Order = 1)]
        public IActionResult Index()
        {
            var roles = _roleManager.GetAllCustomRolesAndUsersCountList();
            return View(roles);
        }

        [AjaxOnly]
        public async Task<IActionResult> RenderRole([FromBody]ModelIdViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Id))
            {
                return PartialView("_Create");
            }

            var role = await _roleManager.FindByIdAsync(model.Id).ConfigureAwait(false);
            if (role == null)
            {
                ModelState.AddModelError("", RoleNotFound);
                return PartialView("_Create");
            }
            return PartialView("_Create", model: new RoleViewModel { Id = role.Id.ToString(), Name = role.Name });
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id).ConfigureAwait(false);
                if (role == null)
                {
                    ModelState.AddModelError("", RoleNotFound);
                }
                else
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role).ConfigureAwait(false);
                    if (result.Succeeded)
                    {
                        return Json(new { success = true });
                    }
                    ModelState.AddErrorsFromResult(result);
                }
            }
            return PartialView("_Create", model: model);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new Role(model.Name)).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
                ModelState.AddErrorsFromResult(result);
            }
            return PartialView("_Create", model: model);
        }

        [AjaxOnly]
        public async Task<IActionResult> RenderDeleteRole([FromBody]ModelIdViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Id))
            {
                return PartialView("_Delete");
            }

            var role = await _roleManager.FindByIdAsync(model.Id).ConfigureAwait(false);
            if (role == null)
            {
                ModelState.AddModelError("", RoleNotFound);
                return PartialView("_Delete");
            }
            return PartialView("_Delete", model: new RoleViewModel { Id = role.Id.ToString(), Name = role.Name });
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id).ConfigureAwait(false);
            if (role == null)
            {
                ModelState.AddModelError("", RoleNotFound);
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
                ModelState.AddErrorsFromResult(result);
            }
            return PartialView("_Delete", model: model);
        }

        [BreadCrumb(Title = "لیست کاربران دارای نقش", Order = 1)]
        public async Task<IActionResult> UsersInRole(int? id, int? page = 1, string field = "Id", SortOrder order = SortOrder.Ascending)
        {
            if (id == null)
            {
                return View("Error");
            }

            var model = await _roleManager.GetPagedApplicationUsersInRoleListAsync(
                roleId: id.Value,
                pageNumber: page.Value - 1,
                recordsPerPage: DefaultPageSize,
                sortByField: field,
                sortOrder: order,
                showAllUsers: true).ConfigureAwait(false);

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
}