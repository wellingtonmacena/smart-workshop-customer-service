using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.List;

public sealed class ListWorkItemsHandler(
    ILogger<ListWorkItemsHandler> logger,
    IWorkItemRepository workItemRepository) : IRequestHandler<ListWorkItemsQuery, Response<IEnumerable<WorkItem>>>
{
    public async Task<Response<IEnumerable<WorkItem>>> Handle(ListWorkItemsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<WorkItem> workItems;

        // Buscar por status se especificado
        if (request.Status.HasValue)
        {
            workItems = await workItemRepository.GetByStatusAsync(request.Status.Value, cancellationToken);
        }
        // Buscar por técnico se especificado
        else if (request.TechnicianId.HasValue)
        {
            workItems = await workItemRepository.GetByTechnicianIdAsync(request.TechnicianId.Value, cancellationToken);
        }
        // Buscar por prioridade se especificado
        else if (request.Priority.HasValue)
        {
            workItems = await workItemRepository.GetByPriorityAsync(request.Priority.Value, cancellationToken);
        }
        // Buscar todos
        else
        {
            workItems = await workItemRepository.GetAllAsync(cancellationToken);
        }

        logger.LogInformation(
            "Listed work items. Count: {Count}, Status: {Status}, TechnicianId: {TechnicianId}, Priority: {Priority}",
            workItems.Count(),
            request.Status,
            request.TechnicianId,
            request.Priority
        );

        return ResponseFactory.Ok(workItems);
    }
}
