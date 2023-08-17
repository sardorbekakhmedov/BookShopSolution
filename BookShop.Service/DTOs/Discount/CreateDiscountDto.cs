using BookShop.Domain.Entities;

namespace BookShop.Service.DTOs.Discount;

public class CreateDiscountDto
{
    public Guid? UserId { get; set; }
    public Guid? PublisherId { get; set; }

    public required Guid BookId { get; set; }
    public float Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}