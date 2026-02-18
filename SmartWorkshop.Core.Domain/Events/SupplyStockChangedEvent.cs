namespace SmartWorkshop.Core.Domain.Events;

public class SupplyStockChangedEvent : DomainEvent
{
    public Guid SupplyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OldQuantity { get; set; }
    public int NewQuantity { get; set; }
    public int Change { get; set; }
}
