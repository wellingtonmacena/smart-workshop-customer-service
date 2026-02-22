using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;

public interface IPersonRepository : IRepository<Person>
{
    Task<Person?> GetByDocumentAsync(string document, CancellationToken cancellationToken = default);
}
