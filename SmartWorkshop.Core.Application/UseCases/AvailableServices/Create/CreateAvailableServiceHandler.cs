using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.AvailableServices.Create;

public sealed class CreateAvailableServiceHandler(
    ILogger<CreateAvailableServiceHandler> logger,
    IMapper mapper,
    IAvailableServiceRepository repository) : IRequestHandler<CreateAvailableServiceCommand, Response<AvailableService>>
{
    public async Task<Response<AvailableService>> Handle(CreateAvailableServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<AvailableService>(request);
        var createdEntity = await repository.AddAsync(entity, cancellationToken);

        logger.LogInformation(
            "AvailableService created. ServiceId: {ServiceId}, Name: {Name}, Price: {Price}",
            createdEntity.Id,
            createdEntity.Name,
            createdEntity.LaborPrice
        );

        return ResponseFactory.Ok(createdEntity, HttpStatusCode.Created);
    }
}
