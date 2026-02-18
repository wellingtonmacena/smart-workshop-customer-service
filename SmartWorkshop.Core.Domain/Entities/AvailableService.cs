namespace SmartWorkshop.Core.Domain.Entities;

public class AvailableService : Common.Entity
{
    private AvailableService() { }

    public AvailableService(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public ICollection<AvailableServiceSupply> AvailableServiceSupplies { get; private set; } = [];

    public AvailableService Update(string? name, decimal? price)
    {
        if (!string.IsNullOrEmpty(name)) Name = name;
        if (price.HasValue) Price = price.Value;
        MarkAsUpdated();
        return this;
    }

    public AvailableService AddSupply(Guid supplyId, int quantity)
    {
        AvailableServiceSupplies.Add(new AvailableServiceSupply(Id, supplyId, quantity));
        MarkAsUpdated();
        return this;
    }

    public AvailableService ClearSupplies()
    {
        AvailableServiceSupplies.Clear();
        MarkAsUpdated();
        return this;
    }
}
