using MediatR;
using Microsoft.Extensions.Logging;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.Vehicles.GetAll;

public sealed class GetAllVehiclesHandler(
    ILogger<GetAllVehiclesHandler> logger,
    IVehicleRepository repository) : IRequestHandler<GetAllVehiclesQuery, Response<IEnumerable<Vehicle>>>
{
    public async Task<Response<IEnumerable<Vehicle>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(cancellationToken);

        logger.LogInformation("Retrieved {Count} vehicles", entities.Count());

        return ResponseFactory.Ok(entities);
    }
}
