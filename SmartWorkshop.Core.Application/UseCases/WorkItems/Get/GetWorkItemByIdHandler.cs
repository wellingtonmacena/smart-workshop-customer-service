using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.Get;

public sealed class GetWorkItemByIdHandler(
    ILogger<GetWorkItemByIdHandler> logger,
    IWorkItemRepository workItemRepository) : IRequestHandler<GetWorkItemByIdQuery, Response<WorkItem>>
{
    public async Task<Response<WorkItem>> Handle(GetWorkItemByIdQuery request, CancellationToken cancellationToken)
    {
        var workItem = await workItemRepository.GetByIdAsync(request.WorkItemId, cancellationToken);

        if (workItem is null)
        {
            logger.LogWarning("Work item not found. WorkItemId: {WorkItemId}", request.WorkItemId);
            return ResponseFactory.Fail<WorkItem>("Work item not found", HttpStatusCode.NotFound);
        }

        return ResponseFactory.Ok(workItem);
    }
}
