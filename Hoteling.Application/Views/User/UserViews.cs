using Hoteling.Domain.Enums;
using Hoteling.Domain.Interfaces;

namespace Hoteling.Application.Views.User;

public class UserCreateView
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // View shouldn't have it.
    public UserRole Role { get; set; }
}

public class UserView : UserCreateView, IHasId
{
    public Guid Id { get; set; }
    public new string Password { get; set; } = string.Empty;
}
