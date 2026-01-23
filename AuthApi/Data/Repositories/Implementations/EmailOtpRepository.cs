using AuthApi.Data.Entities;
using AuthApi.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Data.Repositories.Implementations;

public class EmailOtpRepository : IEmailOtpRepository
{
    private readonly AppDbContext _dbContext;

    public EmailOtpRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddOtpAsync(EmailOtp otp)
    {
        await _dbContext.EmailOtps.AddAsync(otp);
    }

    public async Task<EmailOtp?> GetLatestValidOtpAsync(string email)
    {
        return await _dbContext.EmailOtps
            .Where(x => x.Email == email && x.IsUsed == false)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}