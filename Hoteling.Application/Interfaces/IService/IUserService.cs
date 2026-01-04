using Hoteling.Domain.Entities;

namespace Hoteling.Application.Interfaces.IService;

public interface IUserService : IService<User>
{
    Task<User?> GetUserByEmail(string email);
}

