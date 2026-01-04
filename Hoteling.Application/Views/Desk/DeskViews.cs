using System.ComponentModel.DataAnnotations;
using Hoteling.Domain.Interfaces;

namespace Hoteling.Application.Views.Desk;

public class DeskCreateView
{
    [Required]
    public string DeskNumber { get; set; } = string.Empty;
    public bool HasDualMonitor { get; set; }
    public bool IsStandingDesk { get; set; }
    public int Floor { get; set; }
}

public class DeskView : DeskCreateView, IHasId
{
    public Guid Id { get; set; }
    public bool IsOccupied { get; set; }
    public DateTime? ReservationDate { get; set; }
    //public Guid? ReservedByUserId { get; set; }
    public string? ReservedByUserName { get; set; }
    public int Actions { get; set; }
}
