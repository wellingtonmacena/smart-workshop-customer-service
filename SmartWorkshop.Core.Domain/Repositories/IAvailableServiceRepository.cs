using SmartWorkshop.Core.Domain.Entities;

namespace SmartWorkshop.Core.Domain.Repositories;

public interface IAvailableServiceRepository
{
    Task<AvailableService?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<AvailableService>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<AvailableService>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<AvailableService> AddAsync(AvailableService service, CancellationToken cancellationToken = default);
    Task<AvailableService> UpdateAsync(AvailableService service, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
