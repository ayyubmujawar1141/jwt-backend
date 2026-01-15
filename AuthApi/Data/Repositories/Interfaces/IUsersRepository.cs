using AuthApi.Data.Entities;

namespace AuthApi.Data.Repositories.Interfaces;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task CreateUser(User user);
    Task UpdateUser(int id, User user);
    Task DeleteUser(int id);

}