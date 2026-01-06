using Hoteling.Application.Interfaces.IRepository;
using Hoteling.Domain.Entities;
using Hoteling.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hoteling.Infastructure.Repositories.Users;


public class UserRepository(AppDbContext dbContext) : CrudRepository<User>(dbContext), IUserRepository
{

    public async Task<User?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email), "Email is required.");

        return await dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentNullException(nameof(username), "Username is required.");

        return await dbContext.Users
            .FirstOrDefaultAsync(u => u.UserName == username);
    }
}
