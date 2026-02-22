using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.CompleteRepair;

public sealed class CompleteRepairHandler(
    ILogger<CompleteRepairHandler> logger,
    IWorkItemRepository workItemRepository) : IRequestHandler<CompleteRepairCommand, Response<WorkItem>>
{
    public async Task<Response<WorkItem>> Handle(CompleteRepairCommand request, CancellationToken cancellationToken)
    {
        var workItem = await workItemRepository.GetByIdAsync(request.WorkItemId, cancellationToken);
        if (workItem is null)
        {
            return ResponseFactory.Fail<WorkItem>("Work item not found", HttpStatusCode.NotFound);
        }

        try
        {
            workItem.CompleteRepair();
            var updatedWorkItem = await workItemRepository.UpdateAsync(workItem, cancellationToken);

            logger.LogInformation(
                "Repair completed. WorkItemId: {WorkItemId}, Status: {Status}",
                updatedWorkItem.Id,
                updatedWorkItem.Status
            );

            return ResponseFactory.Ok(updatedWorkItem);
        }
        catch (DomainException ex)
        {
            return ResponseFactory.Fail<WorkItem>(ex.Message, HttpStatusCode.BadRequest);
        }
    }
}
