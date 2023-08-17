using System.Text;
using BookShop.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static void AddJwtValidator(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("JwtBearer");
        services.Configure<JwtOption>(section);

        var jwtOption = section.Get<JwtOption>();

        if (jwtOption is null)
            throw new ArgumentNullException(nameof(JwtOption));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var signingKey = Encoding.UTF8.GetBytes(jwtOption.SigningKey);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtOption.ValidIssuer,
                    ValidAudience = jwtOption.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });
    }
}