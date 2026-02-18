using MediatR;
using SmartWorkshop.Core.Domain.Entities;

namespace SmartWorkshop.Core.Application.Queries.GetAllPeople;

public record GetAllPeopleQuery : IRequest<IEnumerable<Person>>;
