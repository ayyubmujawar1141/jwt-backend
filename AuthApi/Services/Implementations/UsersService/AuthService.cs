using AuthApi.Data.Entities;
using AuthApi.Data.Repositories.Interfaces;
using AuthApi.Dtos.RequestDtos.Auths;
using AuthApi.Dtos.ResponseDtos.Auths;
using AuthApi.Dtos.ResponseDtos.Users;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AuthApi.Services.Implementations.UsersService;

public class AuthService : IAuthService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtService _jwtService;
    private readonly IEmailService _emailService;

    public AuthService(IUsersRepository usersRepository, IJwtService jwtService, IEmailService emailService)
    {
        _usersRepository = usersRepository;
        _jwtService = jwtService;
        _emailService = emailService;
    }
    public async Task<SignupResponseDto> Signup(SignupRequestDto request)
    {
        //Validation
        if (string.IsNullOrWhiteSpace(request.Name) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.ConfirmPassword))
        {
            return new SignupResponseDto
            {
                Success = false,
                Message = "All fields are required."
            };
        }
        //check email already exists
        var exists = await _usersRepository.GetUserByEmail(request.Email);
        if (exists)
        {
            return new SignupResponseDto
            {
                Success = false,
                Message = "Email already exists."
            };
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password

        };
        await _usersRepository.CreateUser(user);
        await _usersRepository.SaveChanges();
        return new SignupResponseDto
        {
            Success = true,
            Message = "Signup Successful"
        };
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return new LoginResponseDto
            {
                Success = false,
                Message = "Email and Password are required."
            };
        }

        var user = await _usersRepository.GetByEmail(request.Email);
        if (user == null)
        {
            return new LoginResponseDto
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        var tokenDto = new UserTokenDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };

        var token = _jwtService.GenerateToken(tokenDto);

        return new LoginResponseDto
        {
            Success = true,
            Message = "Login Successful",
            Token = token,
            User = new UserDto()
            {
                Email = user.Email,
                Name = user.Name
            }
        };
    }


}