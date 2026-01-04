using Hoteling.Domain.Interfaces;

namespace Hoteling.Domain.Entities;

public class Desk : IHasId
{
    public Guid Id { get; set; }
    public string DeskNumber { get; set; } = string.Empty;
    public bool HasDualMonitor { get; set;}
    public bool IsStandingDesk { get; set;}
    public int  Floor { get; set; }
}