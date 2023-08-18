using System.Security.Claims;
using BookShop.Domain.Shared;

namespace BookShop.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static void AddAuthorizationWithRoles(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(UserRoles.SuperAdmin, builder =>
            {
                builder.RequireAssertion(handler =>
                    handler.User.HasClaim(ClaimTypes.Role, UserRoles.SuperAdmin));
            });

            options.AddPolicy(UserRoles.Admin, builder =>
            {
                builder.RequireAssertion(handler =>
                    handler.User.HasClaim(ClaimTypes.Role, UserRoles.Admin)
                    || handler.User.HasClaim(ClaimTypes.Role, UserRoles.SuperAdmin));
            });

            options.AddPolicy(UserRoles.Manager, builder =>
            {
                builder.RequireAssertion(handler =>
                    handler.User.HasClaim(ClaimTypes.Role, UserRoles.SuperAdmin)
                    || handler.User.HasClaim(ClaimTypes.Role, UserRoles.Admin)
                    || handler.User.HasClaim(ClaimTypes.Role, UserRoles.Manager));
            });

            options.AddPolicy(UserRoles.User, builder =>
            {
                builder.RequireAssertion(handler =>
                    handler.User.HasClaim(ClaimTypes.Role, UserRoles.SuperAdmin)
                    || handler.User.HasClaim(ClaimTypes.Role, UserRoles.Admin)
                    || handler.User.HasClaim(ClaimTypes.Role, UserRoles.Manager)
                    || handler.User.HasClaim(ClaimTypes.Role, UserRoles.User));
            });
        });
    }
}