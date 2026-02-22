using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;

namespace SmartWorkshop.Workshop.Application.UseCases.AvailableServices.Create;

public record CreateAvailableServiceCommand(
    string Name,
    decimal Price) : IRequest<Response<AvailableService>>;
