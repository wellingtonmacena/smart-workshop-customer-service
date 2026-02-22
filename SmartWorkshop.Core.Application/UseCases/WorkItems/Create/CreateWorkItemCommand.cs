using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.ValueObjects;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.Create;

public sealed record CreateWorkItemCommand(
    Guid ServiceOrderId,
    WorkItemPriority Priority = WorkItemPriority.Medium
) : IRequest<Response<WorkItem>>;
