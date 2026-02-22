using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;

namespace SmartWorkshop.Workshop.Application.UseCases.Vehicles.Create;

public record CreateVehicleCommand(
    string Model,
    string Brand,
    int ManufactureYear,
    string LicensePlate,
    Guid PersonId) : IRequest<Response<Vehicle>>;
