using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.StartRepair;

public sealed record StartRepairCommand(Guid WorkItemId) : IRequest<Response<WorkItem>>;
