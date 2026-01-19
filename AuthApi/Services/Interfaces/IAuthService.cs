using AuthApi.Dtos.RequestDtos.Auths;
using AuthApi.Dtos.ResponseDtos.Auths;

namespace AuthApi.Services.Interfaces;

public interface IAuthService
{
    Task<SignupResponseDto> Signup(SignupRequestDto request);
    Task<LoginResponseDto> Login(LoginRequestDto request);
}