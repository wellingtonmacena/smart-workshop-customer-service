using SmartWorkshop.Core.Domain.Entities;

namespace SmartWorkshop.Core.Domain.Repositories;

public interface ISupplyRepository
{
    Task<Supply?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Supply>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Supply>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Supply>> GetLowStockAsync(int threshold, CancellationToken cancellationToken = default);
    Task<Supply> AddAsync(Supply supply, CancellationToken cancellationToken = default);
    Task<Supply> UpdateAsync(Supply supply, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
