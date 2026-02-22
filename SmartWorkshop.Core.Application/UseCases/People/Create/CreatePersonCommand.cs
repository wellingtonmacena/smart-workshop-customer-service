using MediatR;
using SmartWorkshop.Workshop.Domain.Common;
using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.ValueObjects;

namespace SmartWorkshop.Workshop.Application.UseCases.People.Create;

public record CreatePersonCommand(
    string Name,
    string DocumentNumber,
    PersonType Type,
    IReadOnlyList<CreateAddressCommand> Addresses,
    IReadOnlyList<CreatePhoneCommand> Phones,
    IReadOnlyList<string> Emails) : IRequest<Response<Person>>;

public record CreateAddressCommand(
    string Street,
    string Number,
    string? Complement,
    string Neighborhood,
    string City,
    string State,
    string ZipCode);

public record CreatePhoneCommand(string Number, PhoneType Type);
