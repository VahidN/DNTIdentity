using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.Services.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Areas.Identity.Controllers;

[Authorize(Roles = ConstantRoles.Admin)]
[Area(AreaConstants.IdentityArea)]
[BreadCrumb(Title = "لاگ سیستم", UseDefaultRouteUrl = true, Order = 0)]
public class SystemLogController(IAppLogItemsService appLogItemsService) : Controller
{
    private readonly IAppLogItemsService _appLogItemsService =
        appLogItemsService ?? throw new ArgumentNullException(nameof(appLogItemsService));

    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public async Task<IActionResult> Index(string logLevel = "",
        int pageNumber = 1,
        int pageSize = -1,
        string sort = "desc")
    {
        var itemsPerPage = 10;

        if (pageSize > 0)
        {
            itemsPerPage = pageSize;
        }

        var model = await _appLogItemsService.GetPagedAppLogItemsAsync(pageNumber, itemsPerPage,
            string.Equals(sort, b: "desc", StringComparison.OrdinalIgnoreCase)
                ? SortOrder.Descending
                : SortOrder.Ascending, logLevel);

        model.LogLevel = logLevel;
        model.Paging.CurrentPage = pageNumber;
        model.Paging.ItemsPerPage = itemsPerPage;

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogItemDelete(int id)
    {
        await _appLogItemsService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogDeleteAll(string logLevel = "")
    {
        await _appLogItemsService.DeleteAllAsync(logLevel);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogDeleteOlderThan(string logLevel = "", int days = 5)
    {
        var cutoffUtc = DateTime.UtcNow.AddDays(-days);
        await _appLogItemsService.DeleteOlderThanAsync(cutoffUtc, logLevel);

        return RedirectToAction(nameof(Index));
    }
}