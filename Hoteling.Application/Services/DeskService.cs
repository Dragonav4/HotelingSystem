using Hoteling.Application.Interfaces.IRepository;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Application.Views.Desk;
using Hoteling.Domain.Entities;

namespace Hoteling.Application.Services;

public class DeskService(
    ICrudRepository<Desk> desksRepository,
    IReservationRepository reservationRepository
) : CrudService<Desk>(desksRepository), IDeskService
{
    private readonly ICrudRepository<Desk> _desksRepository = desksRepository;

    public async Task<List<DeskMapBaseView>> GetMapAsync(DateTime? date, bool isGuest, CancellationToken cancellationToken)
    {
        var targetDate = date ?? DateTime.Today;
        var desks = await _desksRepository.GetAllAsync(cancellationToken: cancellationToken);
        var reservations = await reservationRepository.GetByDateAsync(targetDate, cancellationToken);

        return desks.Select(desk =>
        {
            var res = reservations.FirstOrDefault(r => r.DeskId == desk.Id);

            if (isGuest)
            {
                return new DeskMapBaseView
                {
                    Id = desk.Id,
                    DeskNumber = desk.DeskNumber,
                    IsOccupied = res != null,
                    Floor = desk.Floor
                };
            }

            return new DeskMapExtendedView
            {
                Id = desk.Id,
                DeskNumber = desk.DeskNumber,
                IsOccupied = res != null,
                Floor = desk.Floor,
                ReservedByUserName = res?.User?.UserName,
                ReservationDate = res?.ReservationDate
            };
        }).ToList();
    }
}
