using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.Complete;

public sealed class CompleteWorkItemHandler(
    ILogger<CompleteWorkItemHandler> logger,
    IWorkItemRepository workItemRepository) : IRequestHandler<CompleteWorkItemCommand, Response<WorkItem>>
{
    public async Task<Response<WorkItem>> Handle(CompleteWorkItemCommand request, CancellationToken cancellationToken)
    {
        var workItem = await workItemRepository.GetByIdAsync(request.WorkItemId, cancellationToken);
        if (workItem is null)
        {
            return ResponseFactory.Fail<WorkItem>("Work item not found", HttpStatusCode.NotFound);
        }

        try
        {
            workItem.StartQualityCheck();
            workItem.Complete();
            
            var updatedWorkItem = await workItemRepository.UpdateAsync(workItem, cancellationToken);

            logger.LogInformation(
                "Work item completed. WorkItemId: {WorkItemId}, Duration: {Duration}",
                updatedWorkItem.Id,
                updatedWorkItem.GetDuration()
            );

            return ResponseFactory.Ok(updatedWorkItem);
        }
        catch (DomainException ex)
        {
            return ResponseFactory.Fail<WorkItem>(ex.Message, HttpStatusCode.BadRequest);
        }
    }
}
