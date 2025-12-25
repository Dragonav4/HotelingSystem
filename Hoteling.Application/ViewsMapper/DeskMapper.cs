using Hoteling.Application.Interfaces;
using Hoteling.Application.Views.Desk;
using Hoteling.Domain.Entities;

namespace Hoteling.Application.ViewsMapper;

public class DeskMapper : ICrudMapper<Desk, DeskCreateView, DeskView>
{
    public Desk MapCreateDtoToDomain(DeskCreateView createDto)
    {
        return new Desk
        {
            Id = Guid.NewGuid(),
            DeskNumber = createDto.DeskNumber,
            Floor = createDto.Floor,
            HasDualMonitor = createDto.HasDualMonitor,
            IsStandingDesk = createDto.IsStandingDesk
        };
    }

    public Desk MapViewToDomain(Guid id, DeskView viewDto)
    {
        viewDto.Id = id;
        return new Desk
        {
            Id = id,
            DeskNumber = viewDto.DeskNumber,
            Floor = viewDto.Floor,
            HasDualMonitor = viewDto.HasDualMonitor,
            IsStandingDesk = viewDto.IsStandingDesk
        };
    }

    public DeskView MapDomainToView(Desk viewDto)
    {
        return new DeskView
        {
            Id = viewDto.Id,
            DeskNumber = viewDto.DeskNumber,
            Floor = viewDto.Floor,
            HasDualMonitor = viewDto.HasDualMonitor,
            IsStandingDesk = viewDto.IsStandingDesk
        };
    }
}
