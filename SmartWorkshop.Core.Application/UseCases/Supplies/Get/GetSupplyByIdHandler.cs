using MediatR;
using Microsoft.Extensions.Logging;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.Supplies.Get;

public sealed class GetSupplyByIdHandler(
    ILogger<GetSupplyByIdHandler> logger,
    ISupplyRepository repository) : IRequestHandler<GetSupplyByIdQuery, Response<Supply>>
{
    public async Task<Response<Supply>> Handle(GetSupplyByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            logger.LogWarning("Supply with ID {Id} not found", request.Id);
            return ResponseFactory.Fail<Supply>($"Supply with ID {request.Id} not found");
        }

        logger.LogInformation("Retrieved supply with ID {Id}", request.Id);

        return ResponseFactory.Ok(entity);
    }
}
