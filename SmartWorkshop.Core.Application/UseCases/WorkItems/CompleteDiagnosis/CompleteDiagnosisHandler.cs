using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.CompleteDiagnosis;

public sealed class CompleteDiagnosisHandler(
    ILogger<CompleteDiagnosisHandler> logger,
    IWorkItemRepository workItemRepository) : IRequestHandler<CompleteDiagnosisCommand, Response<WorkItem>>
{
    public async Task<Response<WorkItem>> Handle(CompleteDiagnosisCommand request, CancellationToken cancellationToken)
    {
        var workItem = await workItemRepository.GetByIdAsync(request.WorkItemId, cancellationToken);
        if (workItem is null)
        {
            return ResponseFactory.Fail<WorkItem>("Work item not found", HttpStatusCode.NotFound);
        }

        try
        {
            workItem.CompleteDiagnosis();
            var updatedWorkItem = await workItemRepository.UpdateAsync(workItem, cancellationToken);

            logger.LogInformation(
                "Diagnosis completed. WorkItemId: {WorkItemId}, Status: {Status}",
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
