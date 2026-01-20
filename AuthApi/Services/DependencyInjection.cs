using AuthApi.Services.Implementations.UsersService;
using AuthApi.Services.Interfaces;
using AuthApi.Settings;

namespace AuthApi.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services,IConfiguration configuration)
    {
        #region Services Configuration

        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IUsersService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IEmailService, EmailService>();
        #endregion

        return services;
    }
}