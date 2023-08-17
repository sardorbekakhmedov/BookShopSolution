using BookShop.Domain.Entities;
using BookShop.Service.DTOs.Discount;

namespace BookShop.Service.Extensions;

public static class DiscountExtensions
{
    public static DiscountDto ToDiscountDto(this Discount discount)
    {
        return new DiscountDto
        {
            PublisherName = discount.Publisher?.Name,
            AuthorName = discount.User?.Firstname,
            BookName = discount.Book.Name,
            Percentage = discount.Percentage,
            StartDate = discount.StartDate,
            EndDate = discount.EndDate
        };
    }
}