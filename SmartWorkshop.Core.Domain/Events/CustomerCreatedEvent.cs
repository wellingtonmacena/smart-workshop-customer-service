namespace SmartWorkshop.Core.Domain.Events;

public class CustomerCreatedEvent : DomainEvent
{
    public Guid CustomerId { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
