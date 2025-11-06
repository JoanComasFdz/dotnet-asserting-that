namespace JoanComasFdz.AssertingThat.Demo;

/// <summary>
/// Sample domain model representing an e-commerce order.
/// Used to demonstrate business-specific assertion extensions.
/// </summary>
public class Order
{
    public string OrderId { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public List<string> Products { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? ShippedAt { get; set; }
}

/// <summary>
/// Order status enumeration for demonstration.
/// </summary>
public enum OrderStatus
{
    Pending,
    PendingPayment,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
