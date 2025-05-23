﻿using ASPNETCoreIdentitySample.Common.IdentityToolkit;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

[Authorize(Roles = ConstantRoles.Admin)]
[Area(AreaConstants.IdentityArea)]
[BreadCrumb(Title = "مدیریت کاربران", UseDefaultRouteUrl = true, Order = 0)]
public class UsersManagerController(IApplicationUserManager userManager, IApplicationRoleManager roleManager)
    : Controller
{
    private const int DefaultPageSize = 7;

    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ActivateUserEmailStat(int userId)
    {
        User thisUser = null;

        var result = await userManager.UpdateUserAndSecurityStampAsync(userId, user =>
        {
            user.EmailConfirmed = true;
            thisUser = user;
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(useHtmlNewLine: true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ChangeUserLockoutMode(int userId, bool activate)
    {
        User thisUser = null;

        var result = await userManager.UpdateUserAndSecurityStampAsync(userId, user =>
        {
            user.LockoutEnabled = activate;
            thisUser = user;
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(useHtmlNewLine: true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ChangeUserRoles(int userId, int[] roleIds)
    {
        User thisUser = null;
        var result = await userManager.AddOrUpdateUserRolesAsync(userId, roleIds, user => thisUser = user);

        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(useHtmlNewLine: true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ChangeUserStat(int userId, bool activate)
    {
        User thisUser = null;

        var result = await userManager.UpdateUserAndSecurityStampAsync(userId, user =>
        {
            user.IsActive = activate;
            thisUser = user;
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(useHtmlNewLine: true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ChangeUserTwoFactorAuthenticationStat(int userId, bool activate)
    {
        User thisUser = null;

        var result = await userManager.UpdateUserAndSecurityStampAsync(userId, user =>
        {
            user.TwoFactorEnabled = activate;
            thisUser = user;
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(useHtmlNewLine: true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> EndUserLockout(int userId)
    {
        User thisUser = null;

        var result = await userManager.UpdateUserAndSecurityStampAsync(userId, user =>
        {
            user.LockoutEnd = null;
            thisUser = user;
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(useHtmlNewLine: true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public async Task<IActionResult> Index(int? page = 1, string field = "Id", SortOrder order = SortOrder.Ascending)
    {
        var model = await userManager.GetPagedUsersListAsync(page.Value - 1, DefaultPageSize, field, order,
            showAllUsers: true);

        model.Paging.CurrentPage = page.Value;
        model.Paging.ItemsPerPage = DefaultPageSize;
        model.Paging.ShowFirstLast = true;

        if (HttpContext.Request.IsAjaxRequest())
        {
            return PartialView(viewName: "_UsersList", model);
        }

        return View(model);
    }

    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> SearchUsers(SearchUsersViewModel model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        var pagedUsersList = await userManager.GetPagedUsersListAsync(model, pageNumber: 0);

        pagedUsersList.Paging.CurrentPage = 1;
        pagedUsersList.Paging.ItemsPerPage = model.MaxNumberOfRows;
        pagedUsersList.Paging.ShowFirstLast = true;

        model.PagedUsersList = pagedUsersList;

        return PartialView(viewName: "_SearchUsers", model);
    }

    private async Task<IActionResult> ReturnUserCardPartialView(User thisUser)
    {
        var roles = await roleManager.GetAllCustomRolesAsync();

        return PartialView(viewName: "~/Areas/Identity/Views/UserCard/_UserCardItem.cshtml", new UserCardItemViewModel
        {
            User = thisUser,
            ShowAdminParts = true,
            Roles = roles,
            ActiveTab = UserCardItemActiveTab.UserAdmin
        });
    }
}