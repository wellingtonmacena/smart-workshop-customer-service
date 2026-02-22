using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;

namespace SmartWorkshop.Workshop.Application.UseCases.People.Get;

public record GetPersonByIdQuery(Guid Id) : IRequest<Response<Person>>;
