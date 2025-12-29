using Hoteling.API.Exceptions;
using Hoteling.Application.Interfaces;
using Hoteling.Application.Interfaces.IService;
using Hoteling.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hoteling.API.Controllers;

public class ActionListView<T>
{
    public int Actions { get; set; }
    public List<T> Items { get; set; }
}
//[Authorize]
public abstract class BaseCrudController<T, TCreateView, TView>
    (IService<T> deskService, ICrudMapper<T, TCreateView, TView> mapper, ILogger logger)
    : ControllerBase
    where T : class, IHasId
    where TView : TCreateView, IHasId
{
    protected readonly ILogger Logger = logger;

    [HttpPost]
    public virtual async Task<ActionResult<TView>> CreateAsync([FromBody] TCreateView createDto)
    {
        Logger.LogInformation("Creating new {EntityName}", typeof(T).Name);
        var input = mapper.MapCreateDtoToDomain(createDto);
        var domain = await deskService.CreateAsync(input);
        OnItemCreated(domain);
        var result = mapper.MapDomainToView(domain);
        Logger.LogInformation("{EntityName} created with ID {Id}", typeof(T).Name, domain.Id);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public virtual async Task<ActionResult<TView>> UpdateAsync([FromRoute] Guid id, [FromBody] TView updateDto)
    {

        Logger.LogInformation("Updating {EntityName} with ID {Id}", typeof(T).Name, id);

        var input = mapper.MapViewToDomain(id, updateDto);
        var domain = await deskService.UpdateAsync(input);

        if (domain == null)
        {
            throw new NotFoundException($"{typeof(T).Name} with ID {id} not found for update");
        }

        OnItemUpdated(domain);
        var result = mapper.MapDomainToView(domain);
        Logger.LogInformation("{EntityName} with ID {Id} updated successfully", typeof(T).Name, id);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
    {
        Logger.LogInformation("Deleting {EntityName} with ID {Id}", typeof(T).Name, id);
        var success = await deskService.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"{typeof(T).Name} with ID {id} not found for deletion");
        }

        Logger.LogInformation("{EntityName} with ID {Id} deleted successfully", typeof(T).Name, id);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public virtual async Task<ActionResult<TView>> GetById([FromRoute] Guid id)
    {
        Logger.LogInformation("Getting {EntityName} with ID {Id}", typeof(T).Name, id);
        var domain = await deskService.GetByIdAsync(id);
        if (domain == null)
        {
            throw new NotFoundException($"{typeof(T).Name} with ID {id} not found");
        }

        var result = mapper.MapDomainToView(domain);
        return Ok(result);
    }

    [HttpGet]
    public virtual async Task<ActionResult<ActionListView<TView>>> GetAll()
    {
        Logger.LogInformation("Getting all {EntityName}s", typeof(T).Name);
        var domain = await deskService.GetAllAsync();
        var items = domain
            .Select(item => mapper.MapDomainToView(item))
            .ToList();
        var result = new ActionListView<TView>
        {
            Items = items,
            Actions = 7, // create + view + update
        };
        return Ok(result);
    }

    protected virtual void OnItemCreated(T item)
    {
    }

    protected virtual void OnItemUpdated(T item)
    {
    }
}
