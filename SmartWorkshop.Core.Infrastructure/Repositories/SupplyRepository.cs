using Microsoft.EntityFrameworkCore;
using SmartWorkshop.Core.Domain.Entities;
using SmartWorkshop.Core.Infrastructure.Persistence;

namespace SmartWorkshop.Core.Infrastructure.Repositories;

public class SupplyRepository
{
    private readonly CoreDbContext _context;

    public SupplyRepository(CoreDbContext context)
    {
        _context = context;
    }

    public async Task<Supply?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Supplies
            .Include(s => s.AvailableServiceSupplies)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Supply>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Supplies.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Supply>> GetLowStockAsync(int threshold, CancellationToken cancellationToken = default)
    {
        return await _context.Supplies
            .Where(s => s.Quantity <= threshold)
            .ToListAsync(cancellationToken);
    }

    public async Task<Supply> AddAsync(Supply supply, CancellationToken cancellationToken = default)
    {
        await _context.Supplies.AddAsync(supply, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return supply;
    }

    public async Task<Supply> UpdateAsync(Supply supply, CancellationToken cancellationToken = default)
    {
        supply.MarkAsUpdated();
        _context.Supplies.Update(supply);
        await _context.SaveChangesAsync(cancellationToken);
        return supply;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var supply = await _context.Supplies.FindAsync([id], cancellationToken);
        if (supply != null)
        {
            _context.Supplies.Remove(supply);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
