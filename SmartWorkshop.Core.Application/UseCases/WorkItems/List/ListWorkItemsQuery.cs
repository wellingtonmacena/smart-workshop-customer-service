using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.ValueObjects;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.List;

public sealed record ListWorkItemsQuery(
    WorkItemStatus? Status = null,
    Guid? TechnicianId = null,
    WorkItemPriority? Priority = null
) : IRequest<Response<IEnumerable<WorkItem>>>;
