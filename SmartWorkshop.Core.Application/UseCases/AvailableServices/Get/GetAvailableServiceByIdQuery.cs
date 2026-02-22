using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;

namespace SmartWorkshop.Workshop.Application.UseCases.AvailableServices.Get;

public record GetAvailableServiceByIdQuery(Guid Id) : IRequest<Response<AvailableService>>;
