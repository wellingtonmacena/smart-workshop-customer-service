using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.Get;

public sealed record GetWorkItemByIdQuery(Guid WorkItemId) : IRequest<Response<WorkItem>>;
