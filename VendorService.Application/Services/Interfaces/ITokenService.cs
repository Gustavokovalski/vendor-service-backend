using VendorService.Application.Mappers;

namespace VendorService.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user);
    }
}
