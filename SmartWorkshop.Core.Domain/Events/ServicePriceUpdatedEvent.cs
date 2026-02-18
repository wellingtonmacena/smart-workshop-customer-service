namespace SmartWorkshop.Core.Domain.Events;

public class ServicePriceUpdatedEvent : DomainEvent
{
    public Guid ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
}
