namespace JoanComasFdz.AssertingThat.Demo;

/// <summary>
/// Repository interface for order persistence operations.
/// Used in demo to show mock verification with AssertingThat.
/// </summary>
public interface IOrderRepository
{
    Order GetById(string orderId);
    void Update(Order order);
    void Save(Order order);
}
