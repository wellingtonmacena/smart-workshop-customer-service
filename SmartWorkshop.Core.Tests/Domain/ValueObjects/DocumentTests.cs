using FluentAssertions;
using SmartWorkshop.Core.Domain.ValueObjects;

namespace SmartWorkshop.Core.Tests.Domain.ValueObjects;

public class DocumentTests
{
    [Theory]
    [InlineData("12345678901")]      // CPF válido
    [InlineData("123.456.789-01")]   // CPF formatado
    [InlineData("12345678000190")]   // CNPJ válido
    [InlineData("12.345.678/0001-90")] // CNPJ formatado
    public void Document_Should_Accept_Valid_CPF_And_CNPJ(string validDocument)
    {
        // Act
        var document = new Document(validDocument);

        // Assert
        document.Value.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("abc")]
    public void Document_Should_Reject_Invalid_Document(string invalidDocument)
    {
        // Act
        var act = () => new Document(invalidDocument);

        // Assert
        act.Should().Throw<Common.DomainException>();
    }

    [Fact]
    public void Document_Should_Support_Implicit_String_Conversion()
    {
        // Arrange
        var document = new Document("12345678901");

        // Act
        string value = document;

        // Assert
        value.Should().NotBeNullOrEmpty();
    }
}
