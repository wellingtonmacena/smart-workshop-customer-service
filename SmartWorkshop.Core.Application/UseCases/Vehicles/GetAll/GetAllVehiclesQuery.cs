using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;

namespace SmartWorkshop.Workshop.Application.UseCases.Vehicles.GetAll;

public record GetAllVehiclesQuery() : IRequest<Response<IEnumerable<Vehicle>>>;
