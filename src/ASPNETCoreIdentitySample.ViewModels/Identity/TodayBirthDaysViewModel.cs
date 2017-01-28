using System.Collections.Generic;
using ASPNETCoreIdentitySample.Entities.Identity;

namespace ASPNETCoreIdentitySample.ViewModels.Identity
{
    public class TodayBirthDaysViewModel
    {
        public List<User> Users { set; get; }

        public AgeStatViewModel AgeStat { set; get; }
    }
}