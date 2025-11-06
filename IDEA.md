# IDEA

The normal `Assert` classes provided by testing frameworks are static and therefore, up until now, not extensible. So one can write `Assert.True(xxx)` or `Assert.Equals(expected, actual)` but one can't write `Asserting.That(instance).DidABusinessThing()`.

Yes, there are plenty of assertion libraries, like the ones in xUnit, FluentAssertions, Shouldly etc. which allows chaining smaller methods. The ones from the base frameworks have the issue from above, while the other ones main issue is the lack of a visual cue: one must write `instance.Is().BiggerThan(xxx)` for example, which the traditional IDEs will shows all in gray font color. This makes the asserting lines look identically as arranging or acting lines, and therefore more difficult to visually see where are asserts in a test.

With a static class entry point, the IDEs will color that `Asserting` in a different color, making it instantly obvious that this line is actually asserting something.

Also, this is not an either choice, both can be used at the same time: one for the clear visual cue, the other one to chain asserting methods after it.

The issue of lack of visual cues may not be so prominent in unit tests, but in integration and end 2 end tests that require long setups it becomes really important to visually differentiate asserting lines.

Therefore i want to create a small library that provides an extensible way to produce asserting methods for unit testing. The namespace and resulting package should be `JoanComasFdz.AssertingThat`.

## Key Benefits

1. **Visual Distinction** - The static class entry point (`Asserting.That()`) is colored differently by IDEs, making assertion lines immediately recognizable and distinct from arrangement/act code, especially valuable in integration and E2E tests with long setups.

2. **Business Language** - Enables writing assertions using domain-specific terminology rather than generic C# syntax, making tests read like specifications (e.g., `Asserting.That(order).HasBeenShipped()` vs `Assert.True(order.Status == OrderStatus.Shipped)`).

3. **Discoverability** - Extension methods on `AssertingThat<T>` provide IntelliSense support for type-specific assertions, making it easy to discover available assertions for any given type.

4. **Composability** - Makes it straightforward to build and share domain-specific assertion libraries across test projects, promoting reusable test infrastructure.

5. **Consistency** - Provides a standardized entry point across different assertion styles while remaining compatible with existing assertion libraries (FluentAssertions, Shouldly, xUnit, etc.).

## Technical Details

- **Target Framework**: .NET Standard 2.0 - provides maximum compatibility across .NET Framework 4.6.1+, .NET Core 2.0+, and all modern .NET versions (5.0+)
- **Package Size**: Minimal (2 classes only)
- **Dependencies**: None (framework-agnostic)

## What does it contain

A static inmutable class called `Asserting`. It provides a generic method called `AssertingThat<T> That<T>(T instanceToAssert)` which returns a record `AssertingThat<T>`.

`AssertingThat<T>` exposes a public, read-only property `T InstanceToAssert`.

This allows developers to write extension methods over a the `AssertingThat<T>` class, which `T InstanceToAssert` property returns the instance to be asserted on. And those extensions can return the `AssertingThat<T>` to allow chaining assertions. But this is not enforced, just up to the developer to do it.

## Expected usage

1. Write extension methods in your test project

```csharp
public static class MyDependencyExtensions
{
    public static AssertingThat<IDependency> HasBeenCalledTimes(this AssertingThat<IDependency> assertingThat, uint times)
    {
        // If using NSubstitute:
        assertingThat.InstanceToAssert.SomeMethod().Received(times).SomeMethod();
        return assertingThat;
    }

    public static AssertingThat<IDependency> SomePropertyIsEmpty(this AssertingThat<IDependency> assertingThat)
    {
        // If using NSubstitute
        assertingThat.InstanceToAssert.SomeProperty().IsEmpty();
        return assertingThat;
    }
}
```

2. Use it in a test inside your test project

```csharp
var dependency = Substitute.For<IDependency>();
  // testing stuff...
Asserting.That(dependency)
    .SomeMethodHasBennCalledTimes(3)
    .SomePropertyIsEmpty();
```
