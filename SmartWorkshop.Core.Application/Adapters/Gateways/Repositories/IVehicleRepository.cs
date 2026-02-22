using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken = default);
}
