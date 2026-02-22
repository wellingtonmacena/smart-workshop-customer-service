using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.Vehicles.Update;

public sealed class UpdateVehicleHandler(
    ILogger<UpdateVehicleHandler> logger,
    IVehicleRepository vehicleRepository,
    IPersonRepository personRepository) : IRequestHandler<UpdateVehicleCommand, Response<Vehicle>>
{
    public async Task<Response<Vehicle>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = await vehicleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return ResponseFactory.Fail<Vehicle>("Vehicle not found", HttpStatusCode.NotFound);
        }

        if (!await personRepository.AnyAsync(x => x.Id == request.PersonId, cancellationToken))
        {
            return ResponseFactory.Fail<Vehicle>("Person not found", HttpStatusCode.NotFound);
        }

        entity.Update(request.ManufactureYear, request.LicensePlate, request.Brand, request.Model);
        var updatedEntity = await vehicleRepository.UpdateAsync(entity, cancellationToken);

        logger.LogInformation(
            "Vehicle updated. VehicleId: {VehicleId}, LicensePlate: {LicensePlate}",
            updatedEntity.Id,
            updatedEntity.LicensePlate
        );

        return ResponseFactory.Ok(updatedEntity);
    }
}
