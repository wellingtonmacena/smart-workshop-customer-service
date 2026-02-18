namespace SmartWorkshop.Core.Domain.Entities;

public class AvailableServiceSupply : Common.Entity
{
    private AvailableServiceSupply() { }

    public AvailableServiceSupply(Guid availableServiceId, Guid supplyId, int quantity)
    {
        AvailableServiceId = availableServiceId;
        SupplyId = supplyId;
        Quantity = quantity;
    }

    public Guid AvailableServiceId { get; private set; }
    public Guid SupplyId { get; private set; }
    public int Quantity { get; private set; }

    public AvailableService AvailableService { get; private set; } = null!;
    public Supply Supply { get; private set; } = null!;

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
        MarkAsUpdated();
    }
}
