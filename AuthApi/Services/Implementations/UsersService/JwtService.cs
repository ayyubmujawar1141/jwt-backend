using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthApi.Dtos.ResponseDtos.Auths;
using AuthApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
namespace AuthApi.Services.Implementations.UsersService;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateToken(UserTokenDto userTokenDto)
    {
        var key = _config["JwtSettings:Key"];
        var issuer = _config["JwtSettings:Issuer"];
        var audience = _config["JwtSettings:Audience"];
        var duration = Convert.ToInt32(_config["JwtSettings:DurationInMinutes"]);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userTokenDto.Id.ToString()),
            new Claim(ClaimTypes.Name, userTokenDto.Name),
            new Claim(ClaimTypes.Email, userTokenDto.Email)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(duration),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}