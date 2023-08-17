using BookShop.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static void AddBookshopDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSnakeCaseNamingConvention()
                //.UseInMemoryDatabase("bookshop_db");
                .UseNpgsql(configuration.GetConnectionString("BookShopDb"));
        });
    }
}