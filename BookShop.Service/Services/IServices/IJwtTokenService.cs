using BookShop.Domain.Entities;

namespace BookShop.Service.Services.IServices;

public interface IJwtTokenService
{
   public (string, double) GenerateNewToken(User user);
}