using Hoteling.Application.Interfaces.IRepository;
using Hoteling.Domain.Entities;
using Hoteling.Infastructure.Data;
using Hoteling.Infastructure.Options;
using Hoteling.Infastructure.Repositories;
using Hoteling.Infastructure.Repositories.Desks;
using Hoteling.Infastructure.Repositories.Reservations;
using Hoteling.Infastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Hoteling.Infastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Options
        services.Configure<DatabaseOptions>(configuration.GetSection("Database"));

        // Database
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseSqlite(dbOptions.ConnectionString);
        });

        // Repositories
        services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));

        services.AddScoped<ICrudRepository<Reservation>, ReservationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDeskRepository, DeskRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        return services;
    }
}
