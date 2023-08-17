using BookShop.Service.Services.Pagination;

namespace BookShop.Service.Services.PageFilters;

public class PublisherFilter : PaginationParams
{
    public string? PublisherName { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}