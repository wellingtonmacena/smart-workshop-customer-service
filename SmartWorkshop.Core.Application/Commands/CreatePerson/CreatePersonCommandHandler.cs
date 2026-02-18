using MediatR;
using SmartWorkshop.Core.Domain.Entities;
using SmartWorkshop.Core.Domain.ValueObjects;
using SmartWorkshop.Core.Infrastructure.Repositories;

namespace SmartWorkshop.Core.Application.Commands.CreatePerson;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Person>
{
    private readonly PersonRepository _personRepository;

    public CreatePersonCommandHandler(PersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Person> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        // Parse PersonType
        var personType = Enum.Parse<PersonType>(request.PersonType);

        // Parse EmployeeRole if applicable
        EmployeeRole? employeeRole = null;
        if (personType == PersonType.Employee && !string.IsNullOrEmpty(request.EmployeeRole))
        {
            employeeRole = Enum.Parse<EmployeeRole>(request.EmployeeRole);
        }

        // Create Address
        var address = new Address(
            street: request.Street,
            city: request.City,
            state: request.State,
            zipCode: request.ZipCode
        );

        // Create Phone
        var phone = new Phone(request.Phone);

        // Create Person using constructor
        var person = new Person(
            fullname: request.Name,
            document: request.Document,
            personType: personType,
            employeeRole: employeeRole,
            email: request.Email,
            password: request.Password ?? string.Empty,
            phone: phone,
            address: address
        );

        return await _personRepository.AddAsync(person, cancellationToken);
    }
}
