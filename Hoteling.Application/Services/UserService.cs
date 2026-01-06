using Hoteling.Application.Exceptions;
using Hoteling.Application.Interfaces.IRepository;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Domain.Entities;

namespace Hoteling.Application.Services;

public class UserService(IUserRepository repository) : CrudService<User>(repository), IUserService
{
    public async Task<User?> GetUserByEmail(string email)
    {
        return await repository.GetByEmailAsync(email);
    }

    public async override Task<User?> UpdateAsync(User updateDto, CancellationToken cancellationToken = default)
    {
        var existingUserWithSameUsername = await repository.GetByUsernameAsync(updateDto.UserName);
        if (existingUserWithSameUsername != null && existingUserWithSameUsername.Id != updateDto.Id)
        {
            throw new UserNameException($"Username '{updateDto.UserName}' is already taken by another user.");
        }

        return await base.UpdateAsync(updateDto, cancellationToken);
    }
}

