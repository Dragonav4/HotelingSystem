using Hoteling.Application.Interfaces.IRepository;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Domain.Interfaces;

namespace Hoteling.Application.Services;

public class CrudService<T>(ICrudRepository<T> repository) : IService<T> where T : class, IHasId
{
    public virtual Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return repository.GetByIdAsync(id, cancellationToken);
    }

    public virtual Task<IReadOnlyList<T>> GetAllAsync(int? skip = null, int? take = null, CancellationToken cancellationToken = default)
    {
        return repository.GetAllAsync(skip, take, cancellationToken);
    }

    public virtual Task<T> CreateAsync(T model, CancellationToken cancellationToken = default)
    {
        return repository.CreateAsync(model, cancellationToken);
    }

    public virtual Task<T?> UpdateAsync(T model, CancellationToken cancellationToken = default)
    {
        return repository.UpdateAsync(model, cancellationToken);
    }

    public virtual Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return repository.DeleteAsync(id, cancellationToken);
    }
}
