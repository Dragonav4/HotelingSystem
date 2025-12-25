using Hoteling.Domain.Interfaces;

namespace Hoteling.Application.Interfaces;

public interface ICrudMapper<TDomain,in TCreateDto, TViewDto> where TViewDto : TCreateDto, IHasId
{
    TDomain MapCreateDtoToDomain(TCreateDto createDto);
    TDomain MapViewToDomain(Guid id, TViewDto viewDto);
    TViewDto MapDomainToView(TDomain viewDto);
}