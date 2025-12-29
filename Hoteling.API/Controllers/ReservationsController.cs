using Hoteling.Application.Interfaces;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Application.Views.Reservation;
using Hoteling.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hoteling.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles="Admin,Employee")]

public class ReservationsController(IService<Reservation> deskService, ICrudMapper<Reservation, ReservationCreateView, ReservationView> mapper, ILogger<ReservationsController> logger)
    : BaseCrudController<Reservation, ReservationCreateView, ReservationView>(deskService, mapper, logger)
{
}
