namespace SmartWorkshop.Core.Domain.Events;

public class VehicleRegisteredEvent : DomainEvent
{
    public Guid VehicleId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int ManufactureYear { get; set; }
    public Guid OwnerId { get; set; }
}
