using AuthApi.Data.Repositories.Interfaces;
using AuthApi.Dtos.ResponseDtos.Users;
using AuthApi.Services.Interfaces;

namespace AuthApi.Services.Implementations.UsersService;

public class UserService : IUsersService
{
    private readonly IUsersRepository _repository;

    public UserService(IUsersRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users =  await _repository.GetAllUsers();
        List<UserDto> returnValue = new List<UserDto>();
        foreach (var user in users)
        {
            returnValue.Add(new UserDto()
            {
                Name = user.Name,
                Email = user.Email,
            });
        }

        return returnValue.ToList();

    }
}