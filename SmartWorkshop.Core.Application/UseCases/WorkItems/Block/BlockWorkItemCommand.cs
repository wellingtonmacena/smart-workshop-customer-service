using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.Block;

public sealed record BlockWorkItemCommand(
    Guid WorkItemId,
    string Reason
) : IRequest<Response<WorkItem>>;
