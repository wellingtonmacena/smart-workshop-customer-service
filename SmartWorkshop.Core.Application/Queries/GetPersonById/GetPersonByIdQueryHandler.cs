using MediatR;
using SmartWorkshop.Core.Domain.Entities;
using SmartWorkshop.Core.Infrastructure.Repositories;

namespace SmartWorkshop.Core.Application.Queries.GetPersonById;

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Person?>
{
    private readonly PersonRepository _personRepository;

    public GetPersonByIdQueryHandler(PersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Person?> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        return await _personRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
