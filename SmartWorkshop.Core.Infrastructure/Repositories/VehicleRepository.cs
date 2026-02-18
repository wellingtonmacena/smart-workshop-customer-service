using Microsoft.EntityFrameworkCore;
using SmartWorkshop.Core.Domain.Entities;
using SmartWorkshop.Core.Infrastructure.Persistence;

namespace SmartWorkshop.Core.Infrastructure.Repositories;

public class VehicleRepository
{
    private readonly CoreDbContext _context;

    public VehicleRepository(CoreDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Vehicles
            .Include(v => v.Person)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken = default)
    {
        return await _context.Vehicles
            .Include(v => v.Person)
            .FirstOrDefaultAsync(v => v.LicensePlate.Value == licensePlate, cancellationToken);
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Vehicles
            .Include(v => v.Person)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Vehicle>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await _context.Vehicles
            .Where(v => v.PersonId == ownerId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Vehicle> AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        await _context.Vehicles.AddAsync(vehicle, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return vehicle;
    }

    public async Task<Vehicle> UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        vehicle.MarkAsUpdated();
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync(cancellationToken);
        return vehicle;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var vehicle = await _context.Vehicles.FindAsync([id], cancellationToken);
        if (vehicle != null)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
