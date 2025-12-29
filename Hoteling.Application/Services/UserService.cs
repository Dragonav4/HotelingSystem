using Hoteling.Application.Interfaces;
using Hoteling.Application.Interfaces.IRepository;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Domain.Entities;

namespace Hoteling.Application.Services;

public class UserService(IUserRepository desksRepository) : CrudService<User>(desksRepository), IUserService
{
    public async Task<User?> GetUserByEmail(string email)
    {
        return await desksRepository.GetByEmailAsync(email);
    }


}

