namespace Hoteling.Application.Interfaces.IService;

public interface IService<TDto> where TDto : class
{
    Task<TDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TDto>> GetAllAsync(int? skip = null, int? take = null,
        CancellationToken cancellationToken = default);
    Task<TDto> CreateAsync(TDto model, CancellationToken cancellationToken = default);
    Task<TDto?> UpdateAsync(TDto model, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}