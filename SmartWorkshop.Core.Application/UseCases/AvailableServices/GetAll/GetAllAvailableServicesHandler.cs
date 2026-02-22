using MediatR;
using Microsoft.Extensions.Logging;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.AvailableServices.GetAll;

public sealed class GetAllAvailableServicesHandler(
    ILogger<GetAllAvailableServicesHandler> logger,
    IAvailableServiceRepository repository) : IRequestHandler<GetAllAvailableServicesQuery, Response<IEnumerable<AvailableService>>>
{
    public async Task<Response<IEnumerable<AvailableService>>> Handle(GetAllAvailableServicesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(cancellationToken);

        logger.LogInformation("Retrieved {Count} available services", entities.Count());

        return ResponseFactory.Ok(entities);
    }
}
