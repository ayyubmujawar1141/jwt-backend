using AuthApi.Dtos.ResponseDtos.Auths;

namespace AuthApi.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(UserTokenDto userTokenDto);
}