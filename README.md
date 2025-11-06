# JoanComasFdz.AssertingThat

A tiny, extensible assertion library that provides visual distinction in test code through a static entry point and domain-specific extension methods.

## Why?

Traditional assertion libraries use static methods (`Assert.True()`, `Assert.Equal()`) which aren't extensible, or use extension methods directly on instances (`order.Should()`) which lack visual distinction in IDEs - everything appears in the same gray color.

**AssertingThat** solves both problems:
- ✨ **Visual Distinction**: The static `Asserting` class is colored differently by IDEs, making assertions instantly recognizable
- 🔧 **Extensibility**: Write custom assertion extension methods for your domain models
- 📖 **Readability**: Express business rules in test language
- 🎯 **Discoverability**: IntelliSense shows type-specific assertions

## Quick Start

### Installation

```bash
dotnet add package JoanComasFdz.AssertingThat
```

### Basic Usage

```csharp
using JoanComasFdz.AssertingThat;

// In your test project, create extension methods:
public static class OrderExtensions
{
    public static AssertingThat<Order> HasBeenShipped(this AssertingThat<Order> assertingThat)
    {
        Assert.Equal(OrderStatus.Shipped, assertingThat.InstanceToAssert.Status);
        return assertingThat;
    }

    public static AssertingThat<Order> HasTotalAmount(this AssertingThat<Order> assertingThat, decimal expected)
    {
        Assert.Equal(expected, assertingThat.InstanceToAssert.TotalAmount);
        return assertingThat;
    }
}

// Use in your tests with clear visual distinction:
[Fact]
public void Order_Processing_Success()
{
    var order = ProcessOrder();

    Asserting.That(order)
        .HasBeenShipped()
        .HasTotalAmount(99.99m);
}
```

## Key Benefits

1. **Visual Distinction** - `Asserting` is colored differently in your IDE, making test assertions stand out
2. **Business Language** - Write assertions that match your domain (e.g., `HasBeenShipped()` vs `Status == Shipped`)
3. **Discoverability** - IntelliSense shows available assertions for each type
4. **Composability** - Chain multiple assertions naturally
5. **Framework Agnostic** - Works with xUnit, NUnit, MSTest, or any assertion library

## Compatibility

- **Target**: .NET Standard 2.0
- **Compatible with**: .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5.0+
- **No dependencies** - Bring your own assertion library

## Learn More

- [GitHub Repository](https://github.com/joancomasfdz/dotnet-asserting-that)
- [Getting Started Guide](https://github.com/joancomasfdz/dotnet-asserting-that/blob/main/docs/getting-started.md)
- [Extension Methods Guide](https://github.com/joancomasfdz/dotnet-asserting-that/blob/main/docs/extending.md)
- [Demo Project](https://github.com/joancomasfdz/dotnet-asserting-that/tree/main/samples/JoanComasFdz.AssertingThat.DemoTest)

## License

Apache-2.0 - See [LICENSE](https://github.com/joancomasfdz/dotnet-asserting-that/blob/main/LICENSE) for details.
