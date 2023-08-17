using BookShop.Service.Services.Pagination;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace BookShop.Service.Extensions;

public static class QueryableExtensions
{
    public static async Task<IEnumerable<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> source,
        HttpContextHelper httpContextHelper, PaginationParams @params)
    {
        var content = JsonConvert.SerializeObject(
                        new PaginationMetaData(
                            source.Count(), 
                            @params.AmountData, 
                            @params.PageNumber));

        httpContextHelper.AddResponseHeader("X-Pagination", content);

        return await source.Skip(@params.AmountData * (@params.PageNumber - 1)).Take(@params.AmountData).ToListAsync();
    }
}