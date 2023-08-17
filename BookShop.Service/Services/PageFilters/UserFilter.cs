using BookShop.Service.Services.Pagination;

namespace BookShop.Service.Services.PageFilters;

public class UserFilter : PaginationParams
{
    public string? Username { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}