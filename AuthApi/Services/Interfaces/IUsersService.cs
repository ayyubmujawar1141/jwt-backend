using AuthApi.Dtos.ResponseDtos.Users;

namespace AuthApi.Services.Interfaces;

public interface IUsersService
{
    Task<IEnumerable<UserDto>> GetUsers();
}