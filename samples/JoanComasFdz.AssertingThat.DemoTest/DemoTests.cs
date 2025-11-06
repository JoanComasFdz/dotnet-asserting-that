using JoanComasFdz.AssertingThat.Demo;
using JoanComasFdz.AssertingThat.DemoTest.Extensions;
using NSubstitute;
using Xunit;

namespace JoanComasFdz.AssertingThat.DemoTest;

/// <summary>
/// Demonstration tests showcasing the AssertingThat library in real-world scenarios.
/// These tests illustrate visual distinction, domain-specific assertions, and method chaining.
/// </summary>
public class DemoTests
{
    /// <summary>
    /// Example 1: E-commerce order processing with business rule assertions.
    /// Demonstrates how domain-specific assertions make tests read like specifications.
    /// </summary>
    [Fact]
    public void Example1_EcommerceOrder_BusinessRuleAssertions()
    {
        // Arrange - Create a sample order
        var order = new Order
        {
            OrderId = "ORD-2025-001",
            Status = OrderStatus.Shipped,
            TotalAmount = 299.99m,
            Products = new List<string> { "Laptop", "Mouse", "Keyboard" },
            CreatedAt = DateTime.UtcNow.AddDays(-2),
            ShippedAt = DateTime.UtcNow.AddHours(-12)
        };

        // Act & Assert - Notice how "Asserting" is visually distinct in your IDE
        // The static class entry point is colored differently, making assertions stand out
        Asserting.That(order)
            .HasBeenShipped()
            .HasTotalAmount(299.99m)
            .ContainsProduct("Laptop")
            .HasAtLeastProducts(3);

        // Compare the visual distinction above with traditional assertions below:
        // Assert.Equal(OrderStatus.Shipped, order.Status);  // All in gray
        // Assert.Equal(299.99m, order.TotalAmount);         // Hard to spot assertions
    }

    /// <summary>
    /// Example 2: User authentication with chained assertions.
    /// Shows how multiple assertions can be chained fluently while maintaining readability.
    /// </summary>
    [Fact]
    public void Example2_UserAuthentication_ChainedAssertions()
    {
        // Arrange - Create a user after successful registration and verification
        var user = new User
        {
            UserId = "USR-12345",
            Email = "john.doe@example.com",
            IsActive = true,
            EmailVerified = true,
            Roles = new List<string> { "User", "Premium", "Administrator" },
            CreatedAt = DateTime.UtcNow.AddMonths(-6),
            LastLoginAt = DateTime.UtcNow.AddMinutes(-15)
        };

        // Act & Assert - Chain multiple domain-specific assertions
        // Each method returns AssertingThat<User> enabling fluent chaining
        Asserting.That(user)
            .IsActive()
            .HasVerifiedEmail()
            .HasRole("Administrator")
            .HasRole("Premium")
            .HasLoggedIn()
            .HasAtLeastRoles(2);

        // This reads like a specification:
        // "Assert that user is active, has verified email, has admin role, etc."
    }

    /// <summary>
    /// Example 3: Integration with NSubstitute for mock verification.
    /// Demonstrates how AssertingThat works alongside popular testing libraries.
    /// </summary>
    [Fact]
    public void Example3_MockVerification_WithNSubstitute()
    {
        // Arrange - Create a mock repository
        var mockOrderRepository = Substitute.For<IOrderRepository>();

        var order = new Order
        {
            OrderId = "ORD-2025-002",
            Status = OrderStatus.Processing,
            TotalAmount = 149.99m,
            Products = ["Book"]
        };

        mockOrderRepository.GetById("ORD-2025-002").Returns(order);

        // Act - Simulate order processing service
        var orderService = new OrderService(mockOrderRepository);
        var result = orderService.ProcessOrder("ORD-2025-002");

        // Assert - Verify the order state with domain assertions
        Asserting.That(result)
            .IsBeingProcessed()
            .HasTotalAmount(149.99m)
            .ContainsProduct("Book");

        // And verify repository interactions with NSubstitute
        mockOrderRepository.Received(1).GetById("ORD-2025-002");
        mockOrderRepository.Received(1).Update(Arg.Is<Order>(o => o.OrderId == "ORD-2025-002"));
    }

    /// <summary>
    /// Bonus Example: Demonstrating assertion clarity in complex scenarios.
    /// Shows how AssertingThat improves readability in tests with long setup/arrange sections.
    /// </summary>
    [Fact]
    public void BonusExample_ComplexScenario_VisualDistinction()
    {
        // Arrange - Long setup typical in integration tests
        var userId = "USR-99999";
        var orderDate = DateTime.UtcNow.AddDays(-30);

        // Simulating a complex order fulfillment workflow
        var order = new Order
        {
            OrderId = "ORD-BULK-2025-100",
            Status = OrderStatus.Delivered,
            TotalAmount = 1249.99m,
            Products = new List<string>
            {
                "Desktop PC",
                "Monitor 27\"",
                "Mechanical Keyboard",
                "Gaming Mouse",
                "USB Hub"
            },
            CreatedAt = orderDate,
            ShippedAt = orderDate.AddDays(2)
        };

        var user = new User
        {
            UserId = userId,
            Email = "bulk.buyer@enterprise.com",
            IsActive = true,
            EmailVerified = true,
            Roles = ["User", "Enterprise", "BulkBuyer"],
            CreatedAt = orderDate.AddMonths(-12),
            LastLoginAt = DateTime.UtcNow.AddHours(-1)
        };

        // Act
        // ... complex business logic ...

        // Assert - The "Asserting" keyword stands out immediately!
        // In a long test with lots of setup, this visual cue is invaluable
        Asserting.That(order)
            .HasBeenDelivered()
            .HasTotalAmount(1249.99m)
            .HasAtLeastProducts(5)
            .ContainsProduct("Desktop PC");

        Asserting.That(user)
            .IsActive()
            .HasVerifiedEmail()
            .HasRole("Enterprise")
            .HasRole("BulkBuyer");
    }
}
