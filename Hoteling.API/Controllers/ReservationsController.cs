using Hoteling.Application.Interfaces;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Application.Views.Reservation;
using Hoteling.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hoteling.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(IService<Reservation> crudService, ICrudMapper<Reservation, ReservationCreateView, ReservationView> mapper, ILogger<ReservationsController> logger)
    : BaseCrudController<Reservation, ReservationCreateView, ReservationView>(crudService, mapper, logger)
{
}
