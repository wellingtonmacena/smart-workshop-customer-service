using Microsoft.EntityFrameworkCore;
using SmartWorkshop.Core.Domain.Entities;
using SmartWorkshop.Core.Infrastructure.Persistence;

namespace SmartWorkshop.Core.Infrastructure.Repositories;

public class AvailableServiceRepository
{
    private readonly CoreDbContext _context;

    public AvailableServiceRepository(CoreDbContext context)
    {
        _context = context;
    }

    public async Task<AvailableService?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.AvailableServices
            .Include(s => s.AvailableServiceSupplies)
                .ThenInclude(ass => ass.Supply)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<AvailableService>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.AvailableServices
            .Include(s => s.AvailableServiceSupplies)
            .ToListAsync(cancellationToken);
    }

    public async Task<AvailableService> AddAsync(AvailableService service, CancellationToken cancellationToken = default)
    {
        await _context.AvailableServices.AddAsync(service, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return service;
    }

    public async Task<AvailableService> UpdateAsync(AvailableService service, CancellationToken cancellationToken = default)
    {
        service.MarkAsUpdated();
        _context.AvailableServices.Update(service);
        await _context.SaveChangesAsync(cancellationToken);
        return service;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var service = await _context.AvailableServices.FindAsync([id], cancellationToken);
        if (service != null)
        {
            _context.AvailableServices.Remove(service);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
