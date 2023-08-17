using BookShop.Service.Services.Pagination;

namespace BookShop.Service.Services.PageFilters;

public class GenreFilter : PaginationParams
{
    public string? GenreName { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}