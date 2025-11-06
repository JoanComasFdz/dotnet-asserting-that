# Getting Started with AssertingThat

This guide will walk you through installing and using JoanComasFdz.AssertingThat in your test projects.

## Table of Contents

- [Installation](#installation)
- [Your First Assertion Extension](#your-first-assertion-extension)
- [Using Your Extensions](#using-your-extensions)
- [Understanding the Benefits](#understanding-the-benefits)
- [Common Patterns](#common-patterns)
- [Next Steps](#next-steps)

## Installation

Install the package via NuGet:

```bash
dotnet add package JoanComasFdz.AssertingThat
```

Or via Package Manager Console:

```powershell
Install-Package JoanComasFdz.AssertingThat
```

Or add directly to your `.csproj`:

```xml
<PackageReference Include="JoanComasFdz.AssertingThat" Version="1.0.0" />
```

## Your First Assertion Extension

Let's create a simple extension method for a `Product` class:

### 1. Define Your Domain Model

```csharp
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsInStock { get; set; }
}
```

### 2. Create Extension Methods

In your test project, create a new file for your extensions:

```csharp
using JoanComasFdz.AssertingThat;
using Xunit; // Or NUnit, MSTest, etc.

namespace YourTestProject.Extensions
{
    public static class ProductAssertionExtensions
    {
        public static AssertingThat<Product> IsInStock(
            this AssertingThat<Product> assertingThat)
        {
            Assert.True(
                assertingThat.InstanceToAssert.IsInStock,
                $"Expected product '{assertingThat.InstanceToAssert.Name}' to be in stock");

            return assertingThat;
        }

        public static AssertingThat<Product> HasPrice(
            this AssertingThat<Product> assertingThat,
            decimal expectedPrice)
        {
            Assert.Equal(
                expectedPrice,
                assertingThat.InstanceToAssert.Price);

            return assertingThat;
        }

        public static AssertingThat<Product> HasName(
            this AssertingThat<Product> assertingThat,
            string expectedName)
        {
            Assert.Equal(
                expectedName,
                assertingThat.InstanceToAssert.Name);

            return assertingThat;
        }
    }
}
```

## Using Your Extensions

Now you can use these extensions in your tests:

```csharp
using JoanComasFdz.AssertingThat;
using YourTestProject.Extensions;
using Xunit;

public class ProductTests
{
    [Fact]
    public void Product_Should_BeInStock_WithCorrectPrice()
    {
        // Arrange
        var product = new Product
        {
            Name = "Laptop",
            Price = 999.99m,
            IsInStock = true
        };

        // Act & Assert
        Asserting.That(product)
            .IsInStock()
            .HasPrice(999.99m)
            .HasName("Laptop");
    }
}
```

## Understanding the Benefits

### Visual Distinction

Notice how `Asserting.That()` stands out in your IDE:

```csharp
// AssertingThat - The static "Asserting" is colored differently
Asserting.That(product).IsInStock();

// vs. Traditional - Everything is the same color
Assert.True(product.IsInStock);

// vs. FluentAssertions - All gray, no visual distinction
product.Should().BeTrue();
```

### Readable Business Language

Your tests read like specifications:

```csharp
// Clear, domain-specific language
Asserting.That(order)
    .HasBeenShipped()
    .HasTotalAmount(199.99m)
    .ContainsProduct("Widget");

// vs. Generic assertions
Assert.Equal(OrderStatus.Shipped, order.Status);
Assert.Equal(199.99m, order.TotalAmount);
Assert.Contains("Widget", order.Products);
```

### Discoverability

IntelliSense shows all available assertions for your type:

```csharp
Asserting.That(product).
                       // IntelliSense shows:
                       // - IsInStock()
                       // - HasPrice(decimal)
                       // - HasName(string)
```

## Common Patterns

### Pattern 1: Boolean Properties

```csharp
public static AssertingThat<Order> IsPaid(
    this AssertingThat<Order> assertingThat)
{
    Assert.True(assertingThat.InstanceToAssert.IsPaid);
    return assertingThat;
}

public static AssertingThat<Order> IsNotPaid(
    this AssertingThat<Order> assertingThat)
{
    Assert.False(assertingThat.InstanceToAssert.IsPaid);
    return assertingThat;
}
```

### Pattern 2: Value Comparisons

```csharp
public static AssertingThat<Order> HasTotalGreaterThan(
    this AssertingThat<Order> assertingThat,
    decimal minimum)
{
    Assert.True(
        assertingThat.InstanceToAssert.Total > minimum,
        $"Expected total > {minimum}, but was {assertingThat.InstanceToAssert.Total}");
    return assertingThat;
}
```

### Pattern 3: Collection Checks

```csharp
public static AssertingThat<Order> ContainsProductCount(
    this AssertingThat<Order> assertingThat,
    int expectedCount)
{
    Assert.Equal(
        expectedCount,
        assertingThat.InstanceToAssert.Products.Count);
    return assertingThat;
}

public static AssertingThat<Order> HasProducts(
    this AssertingThat<Order> assertingThat)
{
    Assert.NotEmpty(assertingThat.InstanceToAssert.Products);
    return assertingThat;
}
```

### Pattern 4: Null Checks

```csharp
public static AssertingThat<Order> HasShippingAddress(
    this AssertingThat<Order> assertingThat)
{
    Assert.NotNull(assertingThat.InstanceToAssert.ShippingAddress);
    return assertingThat;
}
```

### Pattern 5: Complex Validations

```csharp
public static AssertingThat<Order> IsReadyToShip(
    this AssertingThat<Order> assertingThat)
{
    var order = assertingThat.InstanceToAssert;

    Assert.True(order.IsPaid, "Order must be paid");
    Assert.NotNull(order.ShippingAddress, "Order must have shipping address");
    Assert.NotEmpty(order.Products, "Order must have products");
    Assert.Equal(OrderStatus.Processing, order.Status);

    return assertingThat;
}
```

## Next Steps

- **Learn More**: Check out the [Extension Methods Guide](extending.md) for advanced techniques
- **See Examples**: Browse the [Demo Test Project](../samples/JoanComasFdz.AssertingThat.DemoTest) for real-world examples
- **Integrate**: Works seamlessly with xUnit, NUnit, MSTest, FluentAssertions, and more

## Tips and Best Practices

1. **Organize by Type**: Create one extension file per domain model type
2. **Return AssertingThat<T>**: Always return the wrapper to enable chaining
3. **Provide Clear Messages**: Include helpful error messages in your assertions
4. **Use Domain Language**: Name methods after business concepts, not implementation details
5. **Keep It Simple**: Each method should verify one business rule

## Troubleshooting

### Extensions Not Showing in IntelliSense?

Make sure you have the correct `using` statements:

```csharp
using JoanComasFdz.AssertingThat;           // For Asserting.That()
using YourTestProject.Extensions;           // For your custom extensions
```

### Build Errors?

Ensure you're using .NET Framework 4.6.1+ or .NET Core 2.0+ (or any modern .NET version).

## Support

- **Issues**: [GitHub Issues](https://github.com/joancomasfdz/dotnet-asserting-that/issues)
- **Discussions**: [GitHub Discussions](https://github.com/joancomasfdz/dotnet-asserting-that/discussions)
- **Source Code**: [GitHub Repository](https://github.com/joancomasfdz/dotnet-asserting-that)
