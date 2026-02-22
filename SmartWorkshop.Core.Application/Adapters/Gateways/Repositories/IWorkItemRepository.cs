using SmartWorkshop.Workshop.Domain.Entities;
using SmartWorkshop.Workshop.Domain.ValueObjects;

namespace SmartWorkshop.Workshop.Application.Adapters.Gateways.Repositories;

public interface IWorkItemRepository : IRepository<WorkItem>
{
    Task<WorkItem?> GetByServiceOrderIdAsync(Guid serviceOrderId, CancellationToken cancellationToken = default);
    Task<IEnumerable<WorkItem>> GetByStatusAsync(WorkItemStatus status, CancellationToken cancellationToken = default);
    Task<IEnumerable<WorkItem>> GetByTechnicianIdAsync(Guid technicianId, CancellationToken cancellationToken = default);
    Task<IEnumerable<WorkItem>> GetByPriorityAsync(WorkItemPriority priority, CancellationToken cancellationToken = default);
}
