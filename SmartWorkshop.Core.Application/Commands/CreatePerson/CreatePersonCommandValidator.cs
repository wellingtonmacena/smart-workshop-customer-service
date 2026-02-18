using FluentValidation;

namespace SmartWorkshop.Core.Application.Commands.CreatePerson;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(200).WithMessage("Nome não pode ter mais de 200 caracteres");

        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("Documento (CPF/CNPJ) é obrigatório");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefone é obrigatório");

        RuleFor(x => x.PersonType)
            .Must(x => x == "Client" || x == "Employee")
            .WithMessage("PersonType deve ser 'Client' ou 'Employee'");

        When(x => x.PersonType == "Employee", () =>
        {
            RuleFor(x => x.EmployeeRole)
                .NotEmpty().WithMessage("EmployeeRole é obrigatório para funcionários");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password é obrigatório para funcionários");
        });

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Rua é obrigatória");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Cidade é obrigatória");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("Estado é obrigatório")
            .Length(2).WithMessage("Estado deve ter 2 caracteres (sigla)");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("CEP é obrigatório");
    }
}
