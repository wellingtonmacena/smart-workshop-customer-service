using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.Common;
using MediatR;

namespace SmartWorkshop.Workshop.Application.UseCases.AvailableServices.GetAll;

public record GetAllAvailableServicesQuery() : IRequest<Response<IEnumerable<AvailableService>>>;
