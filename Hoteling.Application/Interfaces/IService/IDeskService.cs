using Hoteling.Application.Views.Desk;
using Hoteling.Domain.Entities;

namespace Hoteling.Application.Interfaces.IService;

public interface IDeskService : IService<Desk>
{
    Task<List<DeskMapBaseView>> GetMapAsync(DateTime? date, bool isGuest, CancellationToken cancellationToken);
}
