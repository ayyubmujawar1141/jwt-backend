namespace AuthApi.Services.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string email, string subject, string resetLink);
}