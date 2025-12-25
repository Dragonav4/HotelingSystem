using Hoteling.Domain.Interfaces;

namespace Hoteling.Application.Views.Reservation;

public class ReservationCreateView
{
    public Guid DeskId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ReservationDate { get; set; }
}

public class ReservationView : ReservationCreateView, IHasId
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
