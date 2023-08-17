using BookShop.Service.Services.Pagination;

namespace BookShop.Service.Services.PageFilters;

public class DiscountFilter : PaginationParams
{
    public float? Percentage { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}