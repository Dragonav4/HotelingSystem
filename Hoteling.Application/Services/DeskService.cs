using Hoteling.Application.Interfaces.IRepository;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Domain.Entities;

namespace Hoteling.Application.Services;

public class DeskService(
    ICrudRepository<Desk> desksRepository)
    : CrudService<Desk>(desksRepository), IDeskService
{
}
