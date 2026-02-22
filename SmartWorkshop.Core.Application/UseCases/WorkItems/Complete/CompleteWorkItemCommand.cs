using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.Complete;

public sealed record CompleteWorkItemCommand(Guid WorkItemId) : IRequest<Response<WorkItem>>;
