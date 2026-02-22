using MediatR;
using Microsoft.Extensions.Logging;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.People.Get;

public sealed class GetPersonByIdHandler(
    ILogger<GetPersonByIdHandler> logger,
    IPersonRepository repository) : IRequestHandler<GetPersonByIdQuery, Response<Person>>
{
    public async Task<Response<Person>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            logger.LogWarning("Person with ID {Id} not found", request.Id);
            return ResponseFactory.Fail<Person>($"Person with ID {request.Id} not found");
        }

        logger.LogInformation("Retrieved person with ID {Id}", request.Id);

        return ResponseFactory.Ok(entity);
    }
}
