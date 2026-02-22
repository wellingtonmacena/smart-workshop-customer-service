using MediatR;
using Microsoft.Extensions.Logging;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.AvailableServices.Get;

public sealed class GetAvailableServiceByIdHandler(
    ILogger<GetAvailableServiceByIdHandler> logger,
    IAvailableServiceRepository repository) : IRequestHandler<GetAvailableServiceByIdQuery, Response<AvailableService>>
{
    public async Task<Response<AvailableService>> Handle(GetAvailableServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            logger.LogWarning("Available service with ID {Id} not found", request.Id);
            return ResponseFactory.Fail<AvailableService>($"Available service with ID {request.Id} not found");
        }

        logger.LogInformation("Retrieved available service with ID {Id}", request.Id);

        return ResponseFactory.Ok(entity);
    }
}
