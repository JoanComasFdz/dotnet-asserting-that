using JoanComasFdz.AssertingThat.Demo;
using Xunit;

namespace JoanComasFdz.AssertingThat.DemoTest.Extensions;

/// <summary>
/// Extension methods providing domain-specific assertions for Order instances.
/// Demonstrates how to create business-rule assertions with clear, readable syntax.
/// </summary>
public static class OrderAssertionExtensions
{
    /// <summary>
    /// Asserts that the order has been shipped.
    /// </summary>
    public static AssertingThat<Order> HasBeenShipped(this AssertingThat<Order> assertingThat)
    {
        Assert.Equal(OrderStatus.Shipped, assertingThat.InstanceToAssert.Status);
        Assert.NotNull(assertingThat.InstanceToAssert.ShippedAt);
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the order has the expected total amount.
    /// </summary>
    public static AssertingThat<Order> HasTotalAmount(this AssertingThat<Order> assertingThat, decimal expected)
    {
        Assert.Equal(expected, assertingThat.InstanceToAssert.TotalAmount);
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the order contains a specific product.
    /// </summary>
    public static AssertingThat<Order> ContainsProduct(this AssertingThat<Order> assertingThat, string productName)
    {
        Assert.Contains(productName, assertingThat.InstanceToAssert.Products);
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the order is pending payment.
    /// </summary>
    public static AssertingThat<Order> IsPendingPayment(this AssertingThat<Order> assertingThat)
    {
        Assert.Equal(OrderStatus.PendingPayment, assertingThat.InstanceToAssert.Status);
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the order has been delivered.
    /// </summary>
    public static AssertingThat<Order> HasBeenDelivered(this AssertingThat<Order> assertingThat)
    {
        Assert.Equal(OrderStatus.Delivered, assertingThat.InstanceToAssert.Status);
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the order contains at least the specified number of products.
    /// </summary>
    public static AssertingThat<Order> HasAtLeastProducts(this AssertingThat<Order> assertingThat, int minimumCount)
    {
        Assert.True(
            assertingThat.InstanceToAssert.Products.Count >= minimumCount,
            $"Expected at least {minimumCount} products but found {assertingThat.InstanceToAssert.Products.Count}");
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the order is in processing status.
    /// </summary>
    public static AssertingThat<Order> IsBeingProcessed(this AssertingThat<Order> assertingThat)
    {
        Assert.Equal(OrderStatus.Processing, assertingThat.InstanceToAssert.Status);
        return assertingThat;
    }
}
