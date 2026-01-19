using AuthApi.Dtos.ResponseDtos.Users;

namespace AuthApi.Dtos.ResponseDtos.Auths;

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public UserDto? User { get; set;}
}