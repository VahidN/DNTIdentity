using System.Collections.Generic;
using ASPNETCoreIdentitySample.Entities.Identity;

namespace ASPNETCoreIdentitySample.ViewModels.Identity
{
    public enum UserCardItemActiveTab
    {
        UserInfo,
        UserAdmin
    }

    public class UserCardItemViewModel
    {
        public User User { set; get; }
        public bool ShowAdminParts { set; get; }
        public List<Role> Roles { get; set; }
        public UserCardItemActiveTab ActiveTab { get; set; }
    }
}