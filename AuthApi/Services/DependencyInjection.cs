using AuthApi.Services.Implementations.UsersService;
using AuthApi.Services.Interfaces;

namespace AuthApi.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        #region Services Configuration

        services.AddScoped<IUsersService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        #endregion

        return services;
    }
}