using AutoMapper;
using SmartWorkshop.Workshop.Application.UseCases.AvailableServices.Create;
using SmartWorkshop.Workshop.Application.UseCases.People.Create;
using SmartWorkshop.Workshop.Application.UseCases.ServiceOrders.Create;
using SmartWorkshop.Workshop.Application.UseCases.Supplies.Create;
using SmartWorkshop.Workshop.Application.UseCases.Vehicles.Create;
using SmartWorkshop.Workshop.Application.UseCases.Vehicles.Update;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.DTOs.AvailableServices;
using SmartWorkshop.Workshop.Domain.DTOs.People;
using SmartWorkshop.Workshop.Domain.DTOs.ServiceOrders;
using SmartWorkshop.Workshop.Domain.DTOs.Supplies;
using SmartWorkshop.Workshop.Domain.DTOs.Vehicles;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace SmartWorkshop.Workshop.Application.Mappers;

[ExcludeFromCodeCoverage]
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Vehicle mappings
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
        CreateMap<Paginate<Vehicle>, Paginate<VehicleDto>>().ReverseMap();
        CreateMap<Vehicle, CreateVehicleCommand>()
            .ReverseMap()
            .ConstructUsing(dest => new Vehicle(dest.Model, dest.Brand, dest.ManufactureYear, dest.LicensePlate, dest.PersonId));
        CreateMap<Vehicle, UpdateVehicleCommand>().ReverseMap();

        // AvailableService mappings
        CreateMap<AvailableService, AvailableServiceDto>().ReverseMap();
        CreateMap<Paginate<AvailableService>, Paginate<AvailableServiceDto>>().ReverseMap();
        CreateMap<AvailableService, CreateAvailableServiceCommand>().ReverseMap();

        // Supply mappings
        CreateMap<Supply, SupplyDto>().ReverseMap();
        CreateMap<Paginate<Supply>, Paginate<SupplyDto>>().ReverseMap();
        CreateMap<Supply, CreateSupplyCommand>().ReverseMap();

        // Person mappings
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<Paginate<Person>, Paginate<PersonDto>>().ReverseMap();
        CreateMap<Person, CreatePersonCommand>().ReverseMap();

        CreateMap<Phone, PhoneDto>().ReverseMap();
        CreateMap<Phone, CreatePhoneCommand>().ReverseMap();

        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Address, CreateAddressCommand>().ReverseMap();

        CreateMap<Email, EmailDto>().ReverseMap();

        // ServiceOrder mappings
        CreateMap<ServiceOrder, ServiceOrderDto>().ReverseMap();
        CreateMap<CreateServiceOrderCommand, ServiceOrder>().ReverseMap();
        CreateMap<Paginate<ServiceOrder>, Paginate<ServiceOrderDto>>().ReverseMap();

        // Quote mappings
        CreateMap<Quote, QuoteDto>().ReverseMap();
        CreateMap<ServiceOrderEvent, ServiceOrderEventDto>().ReverseMap();
    }
}
