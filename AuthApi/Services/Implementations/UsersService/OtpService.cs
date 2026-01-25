using System.Security.Cryptography;
using System.Text;
using AuthApi.Data.Entities;
using AuthApi.Dtos.RequestDtos.Auths;
using AuthApi.Dtos.ResponseDtos.Auths;
using AuthApi.Data.Repositories.Interfaces;
using AuthApi.Services.Interfaces;

namespace AuthApi.Services.Implementations.UsersService;

public class OtpService : IOtpService
{
    private readonly IEmailOtpRepository _otpRepo;
    private readonly IEmailService _emailService;
    private readonly IUsersRepository _usersRepository;
    
    public OtpService(IEmailOtpRepository otpRepo, IEmailService emailService,  IUsersRepository usersRepository)
    {
        _otpRepo = otpRepo;
        _emailService = emailService;
        _usersRepository = usersRepository;
    }

    public async Task<BasicResponseDto> SendOtpAsync(SendOtpRequestDto requestDto)
    {
        if (string.IsNullOrWhiteSpace(requestDto.Email))
            return new BasicResponseDto { Success = false, Message = "Email is required" };
        var result = await _usersRepository.GetByEmail(requestDto.Email);
        if (result!=null)
        {
            var otp = GenerateOtp();
            var otpHash = Sha256Hash(otp);

            var otpEntity = new EmailOtp
            {
                Email = requestDto.Email,
                OtpHash = otpHash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow

            };
            await _otpRepo.AddOtpAsync(otpEntity);
            await _otpRepo.SaveChangesAsync();

            await _emailService.SendEmailAsync(requestDto.Email, otp);

            return new BasicResponseDto { Success = true, Message = "OTP Sent Successfully" };
        }
        else
        {
            return new BasicResponseDto { Success = false, Message = "Email never exists" };
        }
        
    }

    public async Task<BasicResponseDto> VerifyOtpAsync(VerifyOtpRequestDto requestDto)
    {
        if(string.IsNullOrWhiteSpace(requestDto.Email) || (string.IsNullOrWhiteSpace(requestDto.Otp)))
            return new BasicResponseDto{ Success = false, Message = "Email and OTP are required" };

        var latestOtp = await _otpRepo.GetLatestValidOtpAsync(requestDto.Email);
        
        if(latestOtp == null)
            return new BasicResponseDto{ Success = false, Message = "Otp not found" };
        
        if(DateTime.UtcNow > latestOtp.ExpiresAt)
            return new BasicResponseDto{Success = false, Message = "Otp Expired Successfully"};

        var otpHash = Sha256Hash(requestDto.Otp);

        if (latestOtp.OtpHash != otpHash)
            return new BasicResponseDto { Success = false, Message = "Invalid OTP" };

        latestOtp.IsUsed = true;
        await _otpRepo.SaveChangesAsync();

        return new BasicResponseDto
        {
            Success = true,
            Message = "OTP verified successfully."
        };
    }

    private string GenerateOtp()
    {
        return RandomNumberGenerator.GetInt32(100000, 999999).ToString();
    }

    private string Sha256Hash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}