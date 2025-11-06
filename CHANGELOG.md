# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.0] - 2025-11-06

### Added
- Initial release of JoanComasFdz.AssertingThat
- `Asserting` static class providing the `That<T>()` entry point
- `AssertingThat<T>` record for wrapping instances with extensible assertion methods
- Full XML documentation for IntelliSense support
- .NET Standard 2.0 target for maximum compatibility (.NET Framework 4.6.1+, .NET Core 2.0+, .NET 5+)
- Comprehensive README with usage examples and key benefits
- Demo project showcasing real-world usage patterns
- Sample extension methods for Order and User domain models
- xUnit test examples demonstrating:
  - E-commerce order processing assertions
  - User authentication and authorization assertions
  - Integration with NSubstitute for mock verification
  - Visual distinction in complex test scenarios

### Documentation
- Getting Started guide for new users
- Extension Methods guide for creating custom assertions
- Complete API documentation via XML comments

[Unreleased]: https://github.com/joancomasfdz/dotnet-asserting-that/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/joancomasfdz/dotnet-asserting-that/releases/tag/v1.0.0
