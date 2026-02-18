using MediatR;
using SmartWorkshop.Core.Domain.Entities;
using SmartWorkshop.Core.Infrastructure.Repositories;

namespace SmartWorkshop.Core.Application.Queries.GetAllPeople;

public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, IEnumerable<Person>>
{
    private readonly PersonRepository _personRepository;

    public GetAllPeopleQueryHandler(PersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IEnumerable<Person>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        return await _personRepository.GetAllAsync(cancellationToken);
    }
}
