using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.WorkItems.CompleteDiagnosis;

public sealed record CompleteDiagnosisCommand(Guid WorkItemId) : IRequest<Response<WorkItem>>;
