# JoanComasFdz.AssertingThat

[![NuGet Version](https://img.shields.io/nuget/v/JoanComasFdz.AssertingThat.svg)](https://www.nuget.org/packages/JoanComasFdz.AssertingThat/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/JoanComasFdz.AssertingThat.svg)](https://www.nuget.org/packages/JoanComasFdz.AssertingThat/)
[![Build Status](https://github.com/JoanComasFdz/dotnet-asserting-that/actions/workflows/build.yml/badge.svg)](https://github.com/JoanComasFdz/dotnet-asserting-that/actions/workflows/build.yml)
[![License](https://img.shields.io/github/license/JoanComasFdz/dotnet-asserting-that.svg)](https://github.com/JoanComasFdz/dotnet-asserting-that/blob/main/LICENSE)

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

## Contributing

Contributions are welcome! Here's how you can help:

### Reporting Issues

Found a bug or have a feature request? Please [open an issue](https://github.com/joancomasfdz/dotnet-asserting-that/issues) with:
- A clear title and description
- Steps to reproduce (for bugs)
- Expected vs actual behavior
- Code samples if applicable

### Submitting Pull Requests

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes and add tests
4. Ensure all tests pass (`dotnet test`)
5. Commit your changes (`git commit -m 'Add amazing feature'`)
6. Push to your branch (`git push origin feature/amazing-feature`)
7. Open a Pull Request

### Development Setup

```bash
# Clone the repository
git clone https://github.com/joancomasfdz/dotnet-asserting-that.git
cd dotnet-asserting-that

# Restore dependencies
dotnet restore

# Build
dotnet build

# Run tests
dotnet test
```

## Support

Need help or have questions?

- 📖 **Documentation**: Check the [docs folder](https://github.com/joancomasfdz/dotnet-asserting-that/tree/main/docs) and [demo project](https://github.com/joancomasfdz/dotnet-asserting-that/tree/main/samples/JoanComasFdz.AssertingThat.DemoTest)
- 🐛 **Bug Reports**: [Open an issue](https://github.com/joancomasfdz/dotnet-asserting-that/issues)
- 💡 **Feature Requests**: [Start a discussion](https://github.com/joancomasfdz/dotnet-asserting-that/discussions)
- ⭐ **Star the repo**: If you find this useful, please consider giving it a star!

## License

Apache-2.0 - See [LICENSE](https://github.com/joancomasfdz/dotnet-asserting-that/blob/main/LICENSE) for details.
