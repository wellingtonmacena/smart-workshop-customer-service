using MediatR;
using SmartWorkshop.Core.Domain.Entities;

namespace SmartWorkshop.Core.Application.Commands.CreatePerson;

public record CreatePersonCommand : IRequest<Person>
{
    public string Name { get; init; } = string.Empty;
    public string Document { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string PersonType { get; init; } = "Client"; // Client ou Employee
    public string? EmployeeRole { get; init; }
    public string? Password { get; init; }

    // Address
    public string Street { get; init; } = string.Empty;
    public string Number { get; init; } = string.Empty;
    public string? Complement { get; init; }
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string ZipCode { get; init; } = string.Empty;
}
