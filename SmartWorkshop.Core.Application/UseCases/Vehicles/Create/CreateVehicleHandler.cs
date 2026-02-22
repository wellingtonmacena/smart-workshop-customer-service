using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;
using System.Net;

namespace SmartWorkshop.Workshop.Application.UseCases.Vehicles.Create;

public sealed class CreateVehicleHandler(
    ILogger<CreateVehicleHandler> logger,
    IMapper mapper,
    IVehicleRepository vehicleRepository,
    IPersonRepository personRepository) : IRequestHandler<CreateVehicleCommand, Response<Vehicle>>
{
    public async Task<Response<Vehicle>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        if (!await personRepository.AnyAsync(x => x.Id == request.PersonId, cancellationToken))
        {
            return ResponseFactory.Fail<Vehicle>("Person not found", HttpStatusCode.NotFound);
        }

        var existingVehicle = await vehicleRepository.GetByLicensePlateAsync(request.LicensePlate, cancellationToken);
        if (existingVehicle is not null)
        {
            return ResponseFactory.Fail<Vehicle>("Vehicle with this license plate already exists", HttpStatusCode.Conflict);
        }

        var entity = mapper.Map<Vehicle>(request);
        var createdEntity = await vehicleRepository.AddAsync(entity, cancellationToken);

        logger.LogInformation(
            "Vehicle created. VehicleId: {VehicleId}, LicensePlate: {LicensePlate}, PersonId: {PersonId}",
            createdEntity.Id,
            createdEntity.LicensePlate,
            createdEntity.PersonId
        );

        return ResponseFactory.Ok(createdEntity, HttpStatusCode.Created);
    }
}
