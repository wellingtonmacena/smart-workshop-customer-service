using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;

namespace SmartWorkshop.Workshop.Application.UseCases.People.GetAll;

public record GetAllPeopleQuery() : IRequest<Response<IEnumerable<Person>>>;
