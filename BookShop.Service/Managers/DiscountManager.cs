using AutoMapper;
using BookShop.Data.Repositories.GenericRepositories;
using BookShop.Domain.Entities;
using BookShop.Service.DTOs.Discount;
using BookShop.Service.Exceptions;
using BookShop.Service.Extensions;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Services.PageFilters;
using BookShop.Service.Services.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Service.Managers;

public class DiscountManager : IDiscountManager
{
    private readonly IMapper _mapper;
    private readonly HttpContextHelper _httpContextHelper;
    private readonly IGenericRepository<Discount> _discountRepository;

    public DiscountManager(IMapper mapper, HttpContextHelper httpContextHelper, IGenericRepository<Discount> discountRepository)
    {
        _mapper = mapper;
        _httpContextHelper = httpContextHelper;
        _discountRepository = discountRepository;
    }

    public async ValueTask<DiscountDto> InsertDiscountAsync(CreateDiscountDto dto)
    {
        var discount = _mapper.Map<Discount>(dto);

        var newDiscount = await _discountRepository.InsertAsync(discount);

        var discountSingle = await GetDiscountWithNavigationByIdAsync(newDiscount.Id);

        return discountSingle.ToDiscountDto();
    }

    public async ValueTask<IEnumerable<DiscountDto>> GetAllDiscountsAsync(DiscountFilter filter)
    {
        var query = _discountRepository.SelectAll()
            .Include(u => u.User)
            .Include(p => p.Publisher)
            .Include(b => b.Book)
            .AsQueryable();

        if (filter.Percentage is not null)
            query = query.Where(d => d.Percentage.Equals(filter.Percentage));

        if (filter.FromDate is not null)
            query = query.Where(d => d.CreatedDate >= filter.FromDate);

        if (filter.ToDate is not null)
            query = query.Where(d => d.CreatedDate <= filter.ToDate);

        var discounts = await query.ToPagedListAsync(_httpContextHelper, filter);

        return discounts.Select(discount => discount.ToDiscountDto());
    }

    public async ValueTask<DiscountDto> GetDiscountByIdAsync(Guid discountId)
    {
        var discount = await GetDiscountWithNavigationByIdAsync(discountId);

        return discount.ToDiscountDto();
    }

    public async ValueTask<DiscountDto> UpdateDiscountAsync(Guid discountId, UpdateDiscountDto dto)
    {
        var discount = await GetDiscountWithNavigationByIdAsync(discountId);

        discount.UserId = dto.UserId ?? discount.UserId;
        discount.PublisherId = dto.PublisherId ?? discount.PublisherId;
        discount.BookId = dto.BookId ?? discount.BookId;
        discount.Percentage = dto.Percentage ?? discount.Percentage;
        discount.StartDate = dto.StartDate ?? discount.StartDate;
        discount.EndDate = dto.EndDate ?? discount.EndDate;

        var updateDiscount = await _discountRepository.UpdateAsync(discount);

        return updateDiscount.ToDiscountDto();
    }

    public async ValueTask DeleteDiscountAsync(Guid discountId)
    {
        var discount = await GetDiscountWithNavigationByIdAsync(discountId);

        await _discountRepository.DeleteAsync(discount);
    }


    private async ValueTask<Discount> GetDiscountWithNavigationByIdAsync(Guid discountId)
    {
        var discount = await _discountRepository.SelectAll()
            .Include(u => u.User)
            .Include(p => p.Publisher)
            .Include(b => b.Book)
            .SingleOrDefaultAsync(d => d.Id.Equals(discountId));

        return discount ?? throw new NotFoundException($"{nameof(Discount)} not found!");
    }
}