using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;

namespace SmartWorkshop.Workshop.Application.UseCases.Supplies.GetAll;

public record GetAllSuppliesQuery() : IRequest<Response<IEnumerable<Supply>>>;
