using AuthApi.Dtos.RequestDtos.Auths;
using AuthApi.Dtos.ResponseDtos.Auths;

namespace AuthApi.Services.Interfaces;

public interface IOtpService
{
    Task<BasicResponseDto> SendOtpAsync(SendOtpRequestDto requestDto);
    Task<BasicResponseDto>VerifyOtpAsync(VerifyOtpRequestDto requestDto);
}