using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookShop.Domain.Entities;
using BookShop.Service.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.Service.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOption _jwtOptions;

    public JwtTokenService(IOptions<JwtOption> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public  (string, double) GenerateNewToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        };

        if(user.Roles is not null && user.Roles.Count > 0)
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        var signingKey = Encoding.UTF8.GetBytes(_jwtOptions.SigningKey);
        var symmetricKey = new SymmetricSecurityKey(signingKey);
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtOptions.ValidIssuer,
            audience: _jwtOptions.ValidAudience,
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes));

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return new(jwtToken, TimeSpan.FromMinutes(_jwtOptions.ExpiresInMinutes).TotalMinutes);
    }
}