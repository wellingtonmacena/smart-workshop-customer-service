using MediatR;
using SmartWorkshop.Core.Domain.Entities;

namespace SmartWorkshop.Core.Application.Queries.GetPersonById;

public record GetPersonByIdQuery(Guid Id) : IRequest<Person?>;
