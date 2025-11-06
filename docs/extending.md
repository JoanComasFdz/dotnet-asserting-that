# Extension Methods Guide

This guide covers advanced techniques for creating powerful, reusable assertion extensions with AssertingThat.

## Table of Contents

- [Best Practices](#best-practices)
- [Advanced Patterns](#advanced-patterns)
- [Integration with Other Libraries](#integration-with-other-libraries)
- [Organizing Extension Libraries](#organizing-extension-libraries)
- [Testing Your Extensions](#testing-your-extensions)
- [Performance Considerations](#performance-considerations)

## Best Practices

### 1. Always Return AssertingThat<T>

This enables method chaining:

```csharp
// ✅ Good - Returns AssertingThat<T>
public static AssertingThat<Order> IsShipped(
    this AssertingThat<Order> assertingThat)
{
    Assert.Equal(OrderStatus.Shipped, assertingThat.InstanceToAssert.Status);
    return assertingThat;
}

// ❌ Bad - Void return breaks chaining
public static void IsShipped(
    this AssertingThat<Order> assertingThat)
{
    Assert.Equal(OrderStatus.Shipped, assertingThat.InstanceToAssert.Status);
}
```

### 2. Use Descriptive Method Names

Name methods after business concepts, not implementation details:

```csharp
// ✅ Good - Business language
.HasBeenShipped()
.IsReadyForDelivery()
.WasCreatedToday()

// ❌ Bad - Implementation language
.StatusEqualsShipped()
.ShippedAtIsNotNull()
.CreatedAtEqualsToday()
```

### 3. Provide Helpful Error Messages

Include context in assertion failures:

```csharp
public static AssertingThat<Product> IsAffordable(
    this AssertingThat<Product> assertingThat,
    decimal budget)
{
    var product = assertingThat.InstanceToAssert;
    Assert.True(
        product.Price <= budget,
        $"Expected '{product.Name}' (${product.Price}) to be within budget (${budget})");
    return assertingThat;
}
```

### 4. Keep Methods Focused

Each method should verify one business rule:

```csharp
// ✅ Good - Focused methods
.IsPaid()
.HasShippingAddress()
.ContainsProducts()

// ❌ Bad - Too much in one method
.IsReadyToProcess() // Checks payment, address, products, status, etc.
```

## Advanced Patterns

### Pattern 1: Conditional Assertions

```csharp
public static AssertingThat<Order> HasDiscountIf(
    this AssertingThat<Order> assertingThat,
    bool shouldHaveDiscount)
{
    var order = assertingThat.InstanceToAssert;

    if (shouldHaveDiscount)
    {
        Assert.True(order.DiscountAmount > 0,
            "Expected order to have a discount");
    }
    else
    {
        Assert.Equal(0, order.DiscountAmount);
    }

    return assertingThat;
}
```

### Pattern 2: Range Assertions

```csharp
public static AssertingThat<Product> HasPriceBetween(
    this AssertingThat<Product> assertingThat,
    decimal min,
    decimal max)
{
    var price = assertingThat.InstanceToAssert.Price;
    Assert.InRange(price, min, max);
    return assertingThat;
}
```

### Pattern 3: Collection Assertions with Predicates

```csharp
public static AssertingThat<Order> HasProductMatching(
    this AssertingThat<Order> assertingThat,
    Predicate<Product> predicate)
{
    var order = assertingThat.InstanceToAssert;
    Assert.Contains(order.Products, product => predicate(product));
    return assertingThat;
}

// Usage:
Asserting.That(order)
    .HasProductMatching(p => p.Price > 100);
```

### Pattern 4: Combining Multiple Checks

```csharp
public static AssertingThat<User> IsValidForLogin(
    this AssertingThat<User> assertingThat)
{
    var user = assertingThat.InstanceToAssert;

    Assert.True(user.IsActive, "User must be active");
    Assert.True(user.EmailVerified, "Email must be verified");
    Assert.False(user.IsLocked, "Account must not be locked");
    Assert.NotNull(user.PasswordHash, "User must have a password");

    return assertingThat;
}
```

### Pattern 5: Parameterized Validations

```csharp
public static AssertingThat<Order> MeetsMinimum(
    this AssertingThat<Order> assertingThat,
    OrderRequirements requirements)
{
    var order = assertingThat.InstanceToAssert;

    Assert.True(
        order.Total >= requirements.MinimumTotal,
        $"Order total must be at least ${requirements.MinimumTotal}");

    Assert.True(
        order.Products.Count >= requirements.MinimumItems,
        $"Order must contain at least {requirements.MinimumItems} items");

    return assertingThat;
}
```

### Pattern 6: Date/Time Assertions

```csharp
public static AssertingThat<Order> WasCreatedWithinLast(
    this AssertingThat<Order> assertingThat,
    TimeSpan duration)
{
    var order = assertingThat.InstanceToAssert;
    var cutoff = DateTime.UtcNow.Subtract(duration);

    Assert.True(
        order.CreatedAt >= cutoff,
        $"Expected order to be created after {cutoff}, but was {order.CreatedAt}");

    return assertingThat;
}

// Usage:
Asserting.That(order).WasCreatedWithinLast(TimeSpan.FromHours(1));
```

## Integration with Other Libraries

### With FluentAssertions

```csharp
using FluentAssertions;

public static AssertingThat<Order> HasValidDetails(
    this AssertingThat<Order> assertingThat)
{
    var order = assertingThat.InstanceToAssert;

    // Use FluentAssertions inside your extension
    order.OrderId.Should().NotBeNullOrEmpty();
    order.Total.Should().BePositive();
    order.Products.Should().NotBeEmpty();

    return assertingThat;
}
```

### With NSubstitute (Mocking)

```csharp
using NSubstitute;

public static AssertingThat<IOrderRepository> ReceivedCallToSave(
    this AssertingThat<IOrderRepository> assertingThat)
{
    var repository = assertingThat.InstanceToAssert;
    repository.Received(1).Save(Arg.Any<Order>());
    return assertingThat;
}

// Usage:
var mockRepo = Substitute.For<IOrderRepository>();
// ... test code ...
Asserting.That(mockRepo).ReceivedCallToSave();
```

### With Moq

```csharp
using Moq;

public static AssertingThat<Mock<IOrderService>> VerifiedOrderProcessing(
    this AssertingThat<Mock<IOrderService>> assertingThat)
{
    var mock = assertingThat.InstanceToAssert;
    mock.Verify(s => s.ProcessOrder(It.IsAny<string>()), Times.Once());
    return assertingThat;
}
```

## Organizing Extension Libraries

### Project Structure

For larger test suites, organize extensions by domain:

```
YourProject.Tests/
├── Extensions/
│   ├── OrderAssertionExtensions.cs
│   ├── UserAssertionExtensions.cs
│   ├── ProductAssertionExtensions.cs
│   └── PaymentAssertionExtensions.cs
├── Tests/
│   ├── OrderTests.cs
│   ├── UserTests.cs
│   └── ProductTests.cs
```

### Shared Extension Library

Create a shared library for reusable assertions across projects:

```xml
<!-- YourCompany.Testing.Assertions.csproj -->
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="JoanComasFdz.AssertingThat" Version="1.0.0" />
    <PackageReference Include="xunit" Version="2.4.2" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\YourCompany.Domain\YourCompany.Domain.csproj" />
  </ItemGroup>
</Project>
```

### Namespace Organization

```csharp
// Organize by domain or feature
namespace YourCompany.Testing.Assertions.Orders
{
    public static class OrderAssertionExtensions { }
}

namespace YourCompany.Testing.Assertions.Users
{
    public static class UserAssertionExtensions { }
}
```

## Testing Your Extensions

Yes, you should test your assertion extensions!

```csharp
public class OrderAssertionExtensionsTests
{
    [Fact]
    public void IsShipped_WhenOrderIsShipped_ShouldNotThrow()
    {
        // Arrange
        var order = new Order { Status = OrderStatus.Shipped };

        // Act & Assert
        var exception = Record.Exception(() =>
            Asserting.That(order).IsShipped());

        Assert.Null(exception);
    }

    [Fact]
    public void IsShipped_WhenOrderIsNotShipped_ShouldThrow()
    {
        // Arrange
        var order = new Order { Status = OrderStatus.Pending };

        // Act & Assert
        Assert.Throws<Xunit.Sdk.EqualException>(() =>
            Asserting.That(order).IsShipped());
    }

    [Fact]
    public void IsShipped_ShouldReturnAssertingThat_ForChaining()
    {
        // Arrange
        var order = new Order
        {
            Status = OrderStatus.Shipped,
            Total = 100m
        };

        // Act & Assert - Verify chaining works
        var result = Asserting.That(order)
            .IsShipped()
            .HasTotal(100m);

        Assert.NotNull(result);
    }
}
```

## Performance Considerations

### 1. Avoid Expensive Operations

```csharp
// ❌ Bad - Expensive LINQ query in every assertion
public static AssertingThat<Order> HasExpensiveProducts(
    this AssertingThat<Order> assertingThat)
{
    var expensiveProducts = assertingThat.InstanceToAssert.Products
        .Where(p => p.Price > 1000)
        .OrderByDescending(p => p.Price)
        .ToList();  // Materializes collection

    Assert.NotEmpty(expensiveProducts);
    return assertingThat;
}

// ✅ Good - Use Any() for existence checks
public static AssertingThat<Order> HasExpensiveProducts(
    this AssertingThat<Order> assertingThat)
{
    Assert.True(
        assertingThat.InstanceToAssert.Products.Any(p => p.Price > 1000),
        "Expected order to have at least one expensive product");
    return assertingThat;
}
```

### 2. Cache Expensive Computations

```csharp
public static AssertingThat<Order> MeetsShippingRequirements(
    this AssertingThat<Order> assertingThat)
{
    var order = assertingThat.InstanceToAssert;

    // Cache expensive calculation
    var totalWeight = order.Products.Sum(p => p.Weight);

    Assert.True(totalWeight <= 50, "Total weight exceeds shipping limit");
    Assert.True(totalWeight > 0, "Order must have weight");

    return assertingThat;
}
```

## Advanced Examples

### Example 1: Fluent Validation Integration

```csharp
using FluentValidation;

public static AssertingThat<Order> PassesValidation(
    this AssertingThat<Order> assertingThat)
{
    var order = assertingThat.InstanceToAssert;
    var validator = new OrderValidator();
    var result = validator.Validate(order);

    Assert.True(result.IsValid,
        $"Validation failed: {string.Join(", ", result.Errors)}");

    return assertingThat;
}
```

### Example 2: Custom Comparison

```csharp
public static AssertingThat<Order> IsEquivalentTo(
    this AssertingThat<Order> assertingThat,
    Order expected,
    params string[] ignoredProperties)
{
    var actual = assertingThat.InstanceToAssert;

    // Custom comparison logic
    if (!ignoredProperties.Contains(nameof(Order.OrderId)))
        Assert.Equal(expected.OrderId, actual.OrderId);

    if (!ignoredProperties.Contains(nameof(Order.Total)))
        Assert.Equal(expected.Total, actual.Total);

    // ... more comparisons

    return assertingThat;
}
```

### Example 3: Async Assertions

```csharp
public static async Task<AssertingThat<Order>> WillBeShippedWithin(
    this AssertingThat<Order> assertingThat,
    TimeSpan timeout)
{
    var order = assertingThat.InstanceToAssert;
    var startTime = DateTime.UtcNow;

    while (order.Status != OrderStatus.Shipped)
    {
        if (DateTime.UtcNow - startTime > timeout)
        {
            Assert.Fail($"Order was not shipped within {timeout}");
        }

        await Task.Delay(100);
        // Re-fetch order status in real scenario
    }

    return assertingThat;
}
```

## Resources

- [Getting Started Guide](getting-started.md)
- [Demo Project](../samples/JoanComasFdz.AssertingThat.DemoTest)
- [GitHub Repository](https://github.com/joancomasfdz/dotnet-asserting-that)
- [API Documentation](../src/JoanComasFdz.AssertingThat)

## Contributing

Have a great extension pattern to share? Consider contributing to the documentation or creating a blog post about your usage!
