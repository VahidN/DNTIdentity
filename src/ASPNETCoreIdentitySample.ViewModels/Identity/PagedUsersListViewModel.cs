using System.Collections.Generic;
using ASPNETCoreIdentitySample.Entities.Identity;
using cloudscribe.Web.Pagination;

namespace ASPNETCoreIdentitySample.ViewModels.Identity
{
    public class PagedUsersListViewModel
    {
        public PagedUsersListViewModel()
        {
            Paging = new PaginationSettings();
        }

        public List<User> Users { get; set; }

        public List<Role> Roles { get; set; }

        public PaginationSettings Paging { get; set; }
    }
}
