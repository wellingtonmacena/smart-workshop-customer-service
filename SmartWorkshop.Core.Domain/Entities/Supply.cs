namespace SmartWorkshop.Core.Domain.Entities;

public class Supply : Common.Entity
{
    private Supply() { }

    public Supply(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public string Name { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public ICollection<AvailableServiceSupply> AvailableServiceSupplies { get; private set; } = [];

    public Supply Update(string? name, decimal? price, int? quantity)
    {
        if (!string.IsNullOrEmpty(name)) Name = name;
        if (price.HasValue) Price = price.Value;
        if (quantity.HasValue) Quantity = quantity.Value;
        MarkAsUpdated();
        return this;
    }

    public Supply RemoveFromStock(int quantity)
    {
        Quantity -= quantity;
        MarkAsUpdated();
        return this;
    }

    public Supply AddToStock(int quantity)
    {
        Quantity += quantity;
        MarkAsUpdated();
        return this;
    }
}
