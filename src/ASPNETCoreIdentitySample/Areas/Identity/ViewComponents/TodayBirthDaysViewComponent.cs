using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIdentitySample.Areas.Identity.ViewComponents;

public class TodayBirthDaysViewComponent(ISiteStatService siteStatService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var usersList = await siteStatService.GetTodayBirthdayListAsync();
        var usersAverageAge = await siteStatService.GetUsersAverageAge();

        return View(viewName: "~/Areas/Identity/Views/Shared/Components/TodayBirthDays/Default.cshtml",
            new TodayBirthDaysViewModel
            {
                Users = usersList,
                AgeStat = usersAverageAge
            });
    }
}