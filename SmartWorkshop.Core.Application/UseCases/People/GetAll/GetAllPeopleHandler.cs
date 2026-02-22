using MediatR;
using Microsoft.Extensions.Logging;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.People.GetAll;

public sealed class GetAllPeopleHandler(
    ILogger<GetAllPeopleHandler> logger,
    IPersonRepository repository) : IRequestHandler<GetAllPeopleQuery, Response<IEnumerable<Person>>>
{
    public async Task<Response<IEnumerable<Person>>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(cancellationToken);

        logger.LogInformation("Retrieved {Count} people", entities.Count());

        return ResponseFactory.Ok(entities);
    }
}
