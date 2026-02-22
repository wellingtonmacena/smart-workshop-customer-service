using AutoMapper;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.Supplies.Create;

public sealed class CreateSupplyHandler(
    ILogger<CreateSupplyHandler> logger,
    IMapper mapper,
    ISupplyRepository repository) : IRequestHandler<CreateSupplyCommand, Response<Supply>>
{
    public async Task<Response<Supply>> Handle(CreateSupplyCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Supply>(request);
        var createdEntity = await repository.AddAsync(entity, cancellationToken);

        logger.LogInformation(
            "Supply created. SupplyId: {SupplyId}, Name: {Name}, Quantity: {Quantity}, Price: {Price}",
            createdEntity.Id,
            createdEntity.Name,
            createdEntity.Quantity,
            createdEntity.Price
        );

        return ResponseFactory.Ok(createdEntity, HttpStatusCode.Created);
    }
}
