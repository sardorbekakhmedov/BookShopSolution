using BookShop.Service.Services.Pagination;

namespace BookShop.Service.Services.PageFilters;

public class BookFilter : PaginationParams
{
    public string? BookName { get; set; }
    public string? AuthorName { get; set; }
    public string? PublisherName { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}