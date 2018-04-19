using System.Collections.Generic;
using ASPNETCoreIdentitySample.Entities.Identity;
using DNTCommon.Web.Core;

namespace ASPNETCoreIdentitySample.ViewModels.Identity
{
    public class DynamicRoleClaimsManagerViewModel
    {
        public string[] ActionIds { set; get; }

        public int RoleId { set; get; }

        public Role RoleIncludeRoleClaims { set; get; }

        public ICollection<MvcControllerViewModel> SecuredControllerActions { set; get; }
    }
}