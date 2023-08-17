namespace BookShop.Service.DTOs.Discount;

public class UpdateDiscountDto
{
    public Guid? UserId { get; set; }
    public Guid? PublisherId { get; set; }

    public required Guid? BookId { get; set; }
    public float? Percentage { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}