using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;

namespace SmartWorkshop.Workshop.Application.UseCases.Vehicles.Get;

public record GetVehicleByIdQuery(Guid Id) : IRequest<Response<Vehicle>>;
