using Hoteling.Application.Interfaces;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Application.Views.Desk;
using Hoteling.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hoteling.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DesksController(IService<Desk> crudService, ICrudMapper<Desk, DeskCreateView, DeskView> mapper, ILogger<DesksController> logger)
    : BaseCrudController<Desk, DeskCreateView, DeskView>(crudService, mapper, logger)
{
}
