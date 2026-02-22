using AutoMapper;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.People.Create;

public sealed class CreatePersonHandler(
    ILogger<CreatePersonHandler> logger,
    IMapper mapper,
    IPersonRepository repository) : IRequestHandler<CreatePersonCommand, Response<Person>>
{
    public async Task<Response<Person>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var existingPerson = await repository.GetByDocumentAsync(request.DocumentNumber, cancellationToken);
        if (existingPerson is not null)
        {
            return ResponseFactory.Fail<Person>("Person with this document already exists", HttpStatusCode.Conflict);
        }

        var entity = mapper.Map<Person>(request);
        var createdEntity = await repository.AddAsync(entity, cancellationToken);

        logger.LogInformation(
            "Person created. PersonId: {PersonId}, Name: {Name}, Document: {Document}",
            createdEntity.Id,
            createdEntity.Fullname,
            createdEntity.Document
        );

        return ResponseFactory.Ok(createdEntity, HttpStatusCode.Created);
    }
}
