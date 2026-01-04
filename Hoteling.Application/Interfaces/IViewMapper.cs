namespace Hoteling.Application.Interfaces;

public interface IViewMapper<in TDomain, out TView> 
{
    TView mapDomainToView(TDomain domain);
}