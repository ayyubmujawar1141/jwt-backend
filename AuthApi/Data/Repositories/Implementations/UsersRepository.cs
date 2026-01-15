using AuthApi.Data.Entities;
using AuthApi.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Data.Repositories.Implementations;

public class UsersRepository: IUsersRepository
{
    private readonly AppDbContext _dbContext;

    public UsersRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        return user;
    }

    public async Task CreateUser(User user)
    {
        
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUser(int id, User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

    }
}