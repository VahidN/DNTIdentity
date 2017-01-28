using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNETCoreIdentitySample.Entities.Identity;
using System.Security.Claims;
using ASPNETCoreIdentitySample.ViewModels.Identity;

namespace ASPNETCoreIdentitySample.Services.Contracts.Identity
{
    public interface ISiteStatService
    {
        Task<List<User>> GetOnlineUsersListAsync(int numbersToTake, int minutesToTake);

        Task<List<User>> GetTodayBirthdayListAsync();

        Task UpdateUserLastVisitDateTimeAsync(ClaimsPrincipal claimsPrincipal);

        Task<AgeStatViewModel> GetUsersAverageAge();
    }
}