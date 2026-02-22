using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.Block;

public sealed class BlockWorkItemHandler(
    ILogger<BlockWorkItemHandler> logger,
    IWorkItemRepository workItemRepository) : IRequestHandler<BlockWorkItemCommand, Response<WorkItem>>
{
    public async Task<Response<WorkItem>> Handle(BlockWorkItemCommand request, CancellationToken cancellationToken)
    {
        var workItem = await workItemRepository.GetByIdAsync(request.WorkItemId, cancellationToken);
        if (workItem is null)
        {
            return ResponseFactory.Fail<WorkItem>("Work item not found", HttpStatusCode.NotFound);
        }

        if (string.IsNullOrWhiteSpace(request.Reason))
        {
            return ResponseFactory.Fail<WorkItem>("Block reason is required", HttpStatusCode.BadRequest);
        }

        try
        {
            workItem.Block(request.Reason);
            var updatedWorkItem = await workItemRepository.UpdateAsync(workItem, cancellationToken);

            logger.LogInformation(
                "Work item blocked. WorkItemId: {WorkItemId}, Reason: {Reason}",
                updatedWorkItem.Id,
                request.Reason
            );

            return ResponseFactory.Ok(updatedWorkItem);
        }
        catch (DomainException ex)
        {
            return ResponseFactory.Fail<WorkItem>(ex.Message, HttpStatusCode.BadRequest);
        }
    }
}
