using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.Create;

public sealed class CreateWorkItemHandler(
    ILogger<CreateWorkItemHandler> logger,
    IWorkItemRepository workItemRepository,
    IServiceOrderRepository serviceOrderRepository) : IRequestHandler<CreateWorkItemCommand, Response<WorkItem>>
{
    public async Task<Response<WorkItem>> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
    {
        // Verificar se a ServiceOrder existe
        var serviceOrder = await serviceOrderRepository.GetByIdAsync(request.ServiceOrderId, cancellationToken);
        if (serviceOrder is null)
        {
            return ResponseFactory.Fail<WorkItem>("Service Order not found", HttpStatusCode.NotFound);
        }

        // Verificar se já existe WorkItem para esta ServiceOrder
        var existingWorkItem = await workItemRepository.GetByServiceOrderIdAsync(request.ServiceOrderId, cancellationToken);
        if (existingWorkItem is not null)
        {
            return ResponseFactory.Fail<WorkItem>(
                "A work item already exists for this service order",
                HttpStatusCode.Conflict);
        }

        // Criar novo WorkItem
        var workItem = new WorkItem(request.ServiceOrderId, request.Priority);
        
        var createdWorkItem = await workItemRepository.AddAsync(workItem, cancellationToken);

        logger.LogInformation(
            "Work item created. WorkItemId: {WorkItemId}, ServiceOrderId: {ServiceOrderId}, Priority: {Priority}",
            createdWorkItem.Id,
            createdWorkItem.ServiceOrderId,
            createdWorkItem.Priority
        );

        return ResponseFactory.Ok(createdWorkItem);
    }
}
