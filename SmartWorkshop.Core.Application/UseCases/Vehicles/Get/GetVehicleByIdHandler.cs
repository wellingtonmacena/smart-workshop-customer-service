using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.Vehicles.Get;

public sealed class GetVehicleByIdHandler(
    ILogger<GetVehicleByIdHandler> logger,
    IVehicleRepository repository) : IRequestHandler<GetVehicleByIdQuery, Response<Vehicle>>
{
    public async Task<Response<Vehicle>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            logger.LogWarning("Vehicle with Id {VehicleId} not found", request.Id);
            return ResponseFactory.Fail<Vehicle>("Vehicle not found", HttpStatusCode.NotFound);
        }

        return ResponseFactory.Ok(entity);
    }
}
