using FluentAssertions;
using Moq;
using SmartWorkshop.Core.Application.Commands.CreatePerson;
using SmartWorkshop.Core.Domain.Entities;
using SmartWorkshop.Core.Infrastructure.Repositories;

namespace SmartWorkshop.Core.Tests.Application.Commands;

public class CreatePersonCommandHandlerTests
{
    private readonly Mock<PersonRepository> _personRepositoryMock;
    private readonly CreatePersonCommandHandler _handler;

    public CreatePersonCommandHandlerTests()
    {
        _personRepositoryMock = new Mock<PersonRepository>(MockBehavior.Strict, null!);
        _handler = new CreatePersonCommandHandler(_personRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Person_Successfully()
    {
        // Arrange
        var command = new CreatePersonCommand
        {
            Name = "João da Silva",
            Document = "12345678901",
            Email = "joao@example.com",
            Phone = "11999998888",
            PersonType = "Client",
            Street = "Rua Teste",
            City = "São Paulo",
            State = "SP",
            ZipCode = "01234-567"
        };

        Person? capturedPerson = null;
        _personRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .Callback<Person, CancellationToken>((p, ct) => capturedPerson = p)
            .ReturnsAsync((Person p, CancellationToken ct) => p);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Fullname.Should().Be(command.Name);
        result.Email.Address.Should().Be(command.Email);
        capturedPerson.Should().NotBeNull();
        _personRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Create_Employee_With_Role_And_Password()
    {
        // Arrange
        var command = new CreatePersonCommand
        {
            Name = "Maria Mecânica",
            Document = "98765432100",
            Email = "maria@workshop.com",
            Phone = "11988887777",
            PersonType = "Employee",
            EmployeeRole = "Mechanic",
            Password = "senha123",
            Street = "Rua Trabalho",
            City = "São Paulo",
            State = "SP",
            ZipCode = "01234-567"
        };

        _personRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person p, CancellationToken ct) => p);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.PersonType.Should().Be(Domain.ValueObjects.PersonType.Employee);
        result.EmployeeRole.Should().Be(Domain.ValueObjects.EmployeeRole.Mechanic);
        result.Password.Should().NotBeNull();
    }
}
