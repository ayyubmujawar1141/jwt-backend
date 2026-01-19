using AuthApi.Data.Entities;
using AuthApi.Dtos.RequestDtos.Auths;

namespace AuthApi.Data.Repositories.Interfaces;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task<User?> GetByEmail(string email);
    Task<bool> GetUserByEmail(string email);
    Task CreateUser(User user);
    Task UpdateUser(int id, User user);
    Task DeleteUser(int id);
    Task SaveChanges();

}