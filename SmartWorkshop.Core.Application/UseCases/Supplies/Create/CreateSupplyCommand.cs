using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;

namespace SmartWorkshop.Workshop.Application.UseCases.Supplies.Create;

public record CreateSupplyCommand(
    string Name,
    int Quantity,
    decimal Price) : IRequest<Response<Supply>>;
