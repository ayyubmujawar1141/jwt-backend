using AuthApi.Data.Entities;

namespace AuthApi.Data.Repositories.Interfaces;

public interface IEmailOtpRepository
{
    Task AddOtpAsync(EmailOtp otp);
    Task<EmailOtp?> GetLatestValidOtpAsync(string email);
    
    Task SaveChangesAsync();
}