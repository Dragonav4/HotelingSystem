using Hoteling.Application.Interfaces.IRepository;
using Hoteling.Domain.Entities;
using Hoteling.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hoteling.Infastructure.Repositories.Reservations;

public class ReservationRepository(AppDbContext dbContext) : CrudRepository<Reservation>(dbContext), IReservationRepository
{
    public override async Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(r => r.Desk)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public override async Task<IReadOnlyList<Reservation>> GetAllAsync(int? skip = null, int? take = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Reservation> query = _dbSet
            .Include(r => r.Desk)
            .Include(r => r.User);

        if (skip.HasValue) query = query.Skip(skip.Value);
        if (take.HasValue) query = query.Take(take.Value);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Reservation>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(r => r.User)
            .Where(r => r.ReservationDate.Date == date.Date)
            .ToListAsync(cancellationToken);
    }
}
