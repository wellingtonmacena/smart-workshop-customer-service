using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.Start;

public sealed class StartWorkItemHandler(
    ILogger<StartWorkItemHandler> logger,
    IWorkItemRepository workItemRepository,
    IPersonRepository personRepository) : IRequestHandler<StartWorkItemCommand, Response<WorkItem>>
{
    public async Task<Response<WorkItem>> Handle(StartWorkItemCommand request, CancellationToken cancellationToken)
    {
        // Buscar o WorkItem
        var workItem = await workItemRepository.GetByIdAsync(request.WorkItemId, cancellationToken);
        if (workItem is null)
        {
            return ResponseFactory.Fail<WorkItem>("Work item not found", HttpStatusCode.NotFound);
        }

        // Verificar se o técnico existe
        var technician = await personRepository.GetByIdAsync(request.TechnicianId, cancellationToken);
        if (technician is null)
        {
            return ResponseFactory.Fail<WorkItem>("Technician not found", HttpStatusCode.NotFound);
        }

        try
        {
            // Iniciar o trabalho
            workItem.Start(request.TechnicianId);

            var updatedWorkItem = await workItemRepository.UpdateAsync(workItem, cancellationToken);

            logger.LogInformation(
                "Work item started. WorkItemId: {WorkItemId}, TechnicianId: {TechnicianId}, Status: {Status}",
                updatedWorkItem.Id,
                updatedWorkItem.AssignedTechnicianId,
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
