using Microsoft.EntityFrameworkCore;
using SmartWorkshop.Core.Domain.Entities;
using SmartWorkshop.Core.Domain.ValueObjects;
using SmartWorkshop.Core.Infrastructure.Persistence;

namespace SmartWorkshop.Core.Infrastructure.Repositories;

public class PersonRepository
{
    private readonly CoreDbContext _context;

    public PersonRepository(CoreDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.People
            .Include(p => p.Address)
            .Include(p => p.Vehicles)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Person?> GetByDocumentAsync(string document, CancellationToken cancellationToken = default)
    {
        return await _context.People
            .Include(p => p.Address)
            .FirstOrDefaultAsync(p => p.Document.Value == document, cancellationToken);
    }

    public async Task<Person?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.People
            .Include(p => p.Address)
            .FirstOrDefaultAsync(p => p.Email.Address == email, cancellationToken);
    }

    public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.People
            .Include(p => p.Address)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Person>> GetClientesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.People
            .Include(p => p.Address)
            .Where(p => p.PersonType == PersonType.Client)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Person>> GetEmployeesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.People
            .Include(p => p.Address)
            .Where(p => p.PersonType == PersonType.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        await _context.People.AddAsync(person, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return person;
    }

    public async Task<Person> UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        person.MarkAsUpdated();
        _context.People.Update(person);
        await _context.SaveChangesAsync(cancellationToken);
        return person;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var person = await _context.People.FindAsync([id], cancellationToken);
        if (person != null)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
