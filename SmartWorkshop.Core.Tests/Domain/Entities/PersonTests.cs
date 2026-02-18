using FluentAssertions;
using SmartWorkshop.Core.Domain.Entities;
using SmartWorkshop.Core.Domain.ValueObjects;

namespace SmartWorkshop.Core.Tests.Domain.Entities;

public class PersonTests
{
    [Fact]
    public void Person_Should_Be_Created_With_Valid_Data()
    {
        // Arrange
        var fullname = "João da Silva";
        var document = "12345678901";
        var email = "joao@example.com";
        var password = "senha123";
        var phone = new Phone("11999998888");
        var address = new Address("Rua Teste", "São Paulo", "SP", "01234-567");

        // Act
        var person = new Person(fullname, document, PersonType.Client, null, email, password, phone, address);

        // Assert
        person.Should().NotBeNull();
        person.Fullname.Should().Be(fullname);
        person.PersonType.Should().Be(PersonType.Client);
        person.Id.Should().NotBeEmpty();
        person.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Person_Should_Update_Email_Successfully()
    {
        // Arrange
        var person = CreateValidPerson();
        var newEmail = "novo@example.com";

        // Act
        person.UpdateEmail(newEmail);

        // Assert
        person.Email.Address.Should().Be(newEmail);
        person.UpdatedAt.Should().NotBeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid-email")]
    [InlineData("@example.com")]
    public void Person_Should_Throw_Exception_For_Invalid_Email(string invalidEmail)
    {
        // Arrange & Act
        var act = () => new Email(invalidEmail);

        // Assert
        act.Should().Throw<Common.DomainException>();
    }

    [Fact]
    public void Person_Should_Update_Phone_Successfully()
    {
        // Arrange
        var person = CreateValidPerson();
        var newPhone = new Phone("21988887777");

        // Act
        person.UpdatePhone(newPhone);

        // Assert
        person.Phone.Should().Be(newPhone);
    }

    private static Person CreateValidPerson()
    {
        var phone = new Phone("11999998888");
        var address = new Address("Rua Teste", "São Paulo", "SP", "01234-567");
        
        return new Person(
            "Test Person",
            "12345678901",
            PersonType.Client,
            null,
            "test@example.com",
            "password123",
            phone,
            address
        );
    }
}
