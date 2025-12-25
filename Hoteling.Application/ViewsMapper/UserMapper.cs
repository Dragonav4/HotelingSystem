using Hoteling.Application.Interfaces;
using Hoteling.Application.Views.User;
using Hoteling.Domain.Entities;

namespace Hoteling.Application.ViewsMapper;

public class UserMapper : ICrudMapper<User, UserCreateView, UserView>
{
    public User MapCreateDtoToDomain(UserCreateView createDto)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = createDto.Email,
            UserName = createDto.UserName,
            // Hashing password
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createDto.Password),
            Role = createDto.Role
        };
    }

    public User MapViewToDomain(Guid id, UserView viewDto)
    {
        viewDto.Id = id;
        return new User
        {
            Id = id,
            Email = viewDto.Email,
            UserName = viewDto.UserName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(viewDto.Password),
            Role = viewDto.Role
        };
    }

    public UserView MapDomainToView(User viewDto)
    {
        return new UserView
        {
            Id = viewDto.Id,
            Email = viewDto.Email,
            UserName = viewDto.UserName,
            Role = viewDto.Role,
            Password = "" // Don't return hash
        };
    }
}
