namespace JoanComasFdz.AssertingThat;

/// <summary>
/// Record type that wraps an instance for assertion operations.
/// Provides a fluent interface for chaining custom assertion extension methods.
/// </summary>
/// <typeparam name="T">Type of the instance being asserted</typeparam>
/// <remarks>
/// This record is designed to be extended via extension methods, enabling domain-specific
/// assertion syntax. Extension methods should typically return <see cref="AssertingThat{T}"/>
/// to allow method chaining.
/// </remarks>
/// <example>
/// <code>
/// public static class OrderExtensions
/// {
///     public static AssertingThat&lt;Order&gt; HasBeenShipped(this AssertingThat&lt;Order&gt; assertingThat)
///     {
///         Assert.Equal(OrderStatus.Shipped, assertingThat.InstanceToAssert.Status);
///         return assertingThat;
///     }
/// }
/// </code>
/// </example>
public record AssertingThat<T>(T InstanceToAssert);

