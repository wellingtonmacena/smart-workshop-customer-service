using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;

namespace SmartWorkshop.Workshop.Application.UseCases.Supplies.Get;

public record GetSupplyByIdQuery(Guid Id) : IRequest<Response<Supply>>;
