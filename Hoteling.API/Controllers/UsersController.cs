using Hoteling.Application.Interfaces;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Application.Views.User;
using Hoteling.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hoteling.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IService<User> crudService, ICrudMapper<User, UserCreateView, UserView> mapper, ILogger<UsersController> logger)
    : BaseCrudController<User, UserCreateView, UserView>(crudService, mapper, logger)
{
}
