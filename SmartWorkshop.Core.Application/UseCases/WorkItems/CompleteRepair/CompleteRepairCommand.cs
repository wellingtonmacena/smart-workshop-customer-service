using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.CompleteRepair;

public sealed record CompleteRepairCommand(Guid WorkItemId) : IRequest<Response<WorkItem>>;
