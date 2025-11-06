namespace JoanComasFdz.AssertingThat;

/// <summary>
/// Static entry point for creating extensible assertions with visual distinction in IDEs.
/// </summary>
/// <remarks>
/// This class provides a static entry point that IDEs will color differently from instance methods,
/// making assertion lines immediately recognizable in test code. Extension methods can be written
/// on <see cref="AssertingThat{T}"/> to provide domain-specific assertion capabilities.
/// </remarks>
/// <example>
/// <code>
/// Asserting.That(order)
///     .HasBeenShipped()
///     .HasTotalAmount(99.99m);
/// </code>
/// </example>
public static class Asserting
{
    /// <summary>
    /// Creates an asserting context for the given instance.
    /// </summary>
    /// <typeparam name="T">Type of the instance to assert</typeparam>
    /// <param name="instanceToAssert">The instance to create assertions for</param>
    /// <returns>An asserting context that can be extended with custom assertion methods</returns>
    public static AssertingThat<T> That<T>(T instanceToAssert) => new(instanceToAssert);
}

