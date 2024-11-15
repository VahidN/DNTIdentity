using ASPNETCoreIdentitySample.Entities.Identity;
using cloudscribe.Web.Pagination;

namespace ASPNETCoreIdentitySample.ViewModels.Identity;

public class PagedAppLogItemsViewModel
{
    public string LogLevel { get; set; } = string.Empty;

    public List<AppLogItem> AppLogItems { get; set; }

    public PaginationSettings Paging { get; set; } = new();
}