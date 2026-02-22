using MediatR;
using Microsoft.Extensions.Logging;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.Supplies.GetAll;

public sealed class GetAllSuppliesHandler(
    ILogger<GetAllSuppliesHandler> logger,
    ISupplyRepository repository) : IRequestHandler<GetAllSuppliesQuery, Response<IEnumerable<Supply>>>
{
    public async Task<Response<IEnumerable<Supply>>> Handle(GetAllSuppliesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(cancellationToken);

        logger.LogInformation("Retrieved {Count} supplies", entities.Count());

        return ResponseFactory.Ok(entities);
    }
}
