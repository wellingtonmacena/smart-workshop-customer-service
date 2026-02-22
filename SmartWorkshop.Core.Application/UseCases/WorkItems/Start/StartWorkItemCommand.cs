using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.Start;

public sealed record StartWorkItemCommand(
    Guid WorkItemId,
    Guid TechnicianId
) : IRequest<Response<WorkItem>>;
