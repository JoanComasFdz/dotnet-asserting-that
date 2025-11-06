namespace JoanComasFdz.AssertingThat.Demo;

/// <summary>
/// Service for processing orders.
/// Demonstrates business logic that can be tested with AssertingThat extensions.
/// </summary>
public class OrderService(IOrderRepository repository)
{
    private readonly IOrderRepository _repository = repository;

    public Order ProcessOrder(string orderId)
    {
        var order = _repository.GetById(orderId);
        order.Status = OrderStatus.Processing;
        _repository.Update(order);
        return order;
    }

    public Order ShipOrder(string orderId)
    {
        var order = _repository.GetById(orderId);
        order.Status = OrderStatus.Shipped;
        order.ShippedAt = DateTime.UtcNow;
        _repository.Update(order);
        return order;
    }
}
