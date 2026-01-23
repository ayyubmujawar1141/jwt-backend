using AuthApi.Data.Repositories.Implementations;
using AuthApi.Data.Repositories.Interfaces;
using AuthApi.Services.Implementations.UsersService;
using AuthApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataLayerServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        #region Repositoy Configuration

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IEmailOtpRepository, EmailOtpRepository>();
        #endregion

       
        return services;
    }
}