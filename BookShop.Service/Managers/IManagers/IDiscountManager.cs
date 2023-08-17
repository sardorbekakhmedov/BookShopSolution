using BookShop.Service.DTOs.Discount;
using BookShop.Service.Services.PageFilters;

namespace BookShop.Service.Managers.IManagers;

public interface IDiscountManager
{
    ValueTask<DiscountDto> InsertDiscountAsync(CreateDiscountDto dto);
    ValueTask<IEnumerable<DiscountDto>> GetAllDiscountsAsync(DiscountFilter filter);
    ValueTask<DiscountDto> GetDiscountByIdAsync(Guid discountId);
    ValueTask<DiscountDto> UpdateDiscountAsync(Guid discountId, UpdateDiscountDto dto);
    ValueTask DeleteDiscountAsync(Guid discountId);

}