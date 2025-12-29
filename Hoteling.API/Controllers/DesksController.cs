using Hoteling.Application.Interfaces;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Application.Views.Desk;
using Hoteling.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hoteling.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Employee")]
public class DesksController(
    IDeskService deskService,
    ICrudMapper<Desk, DeskCreateView, DeskView> mapper,
    ILogger<DesksController> logger)
    : BaseCrudController<Desk, DeskCreateView, DeskView>(deskService, mapper, logger)
{
    [HttpGet("map")]
    [AllowAnonymous]
    public async Task<IActionResult> GetMap([FromQuery] DateTime? date, CancellationToken cancellationToken)
    {
        var isGuest = !User.Identity?.IsAuthenticated ?? true;
        var getMap = await deskService.GetMapAsync(date, isGuest,cancellationToken);
        var result = new
        {
            actions = 7,
            getMap,
        };
        return Ok(result);
    }
}
