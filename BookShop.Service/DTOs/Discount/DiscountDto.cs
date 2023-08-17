namespace BookShop.Service.DTOs.Discount;

public class DiscountDto
{
    public string? AuthorName { get; set; }
    public string? PublisherName { get; set; }

    public required string BookName { get; set; }
    public float Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}