using JoanComasFdz.AssertingThat.Demo;
using Xunit;

namespace JoanComasFdz.AssertingThat.DemoTest.Extensions;

/// <summary>
/// Extension methods providing domain-specific assertions for User instances.
/// Demonstrates authentication and authorization assertion patterns.
/// </summary>
public static class UserAssertionExtensions
{
    /// <summary>
    /// Asserts that the user account is active.
    /// </summary>
    public static AssertingThat<User> IsActive(this AssertingThat<User> assertingThat)
    {
        Assert.True(assertingThat.InstanceToAssert.IsActive, "Expected user to be active");
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the user account is inactive.
    /// </summary>
    public static AssertingThat<User> IsInactive(this AssertingThat<User> assertingThat)
    {
        Assert.False(assertingThat.InstanceToAssert.IsActive, "Expected user to be inactive");
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the user has the specified role.
    /// </summary>
    public static AssertingThat<User> HasRole(this AssertingThat<User> assertingThat, string roleName)
    {
        Assert.Contains(roleName, assertingThat.InstanceToAssert.Roles);
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the user email has been verified.
    /// </summary>
    public static AssertingThat<User> HasVerifiedEmail(this AssertingThat<User> assertingThat)
    {
        Assert.True(assertingThat.InstanceToAssert.EmailVerified, "Expected email to be verified");
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the user email has not been verified.
    /// </summary>
    public static AssertingThat<User> HasUnverifiedEmail(this AssertingThat<User> assertingThat)
    {
        Assert.False(assertingThat.InstanceToAssert.EmailVerified, "Expected email to be unverified");
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the user has logged in at least once.
    /// </summary>
    public static AssertingThat<User> HasLoggedIn(this AssertingThat<User> assertingThat)
    {
        Assert.NotNull(assertingThat.InstanceToAssert.LastLoginAt);
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the user has never logged in.
    /// </summary>
    public static AssertingThat<User> HasNeverLoggedIn(this AssertingThat<User> assertingThat)
    {
        Assert.Null(assertingThat.InstanceToAssert.LastLoginAt);
        return assertingThat;
    }

    /// <summary>
    /// Asserts that the user has at least the specified number of roles.
    /// </summary>
    public static AssertingThat<User> HasAtLeastRoles(this AssertingThat<User> assertingThat, int minimumCount)
    {
        Assert.True(
            assertingThat.InstanceToAssert.Roles.Count >= minimumCount,
            $"Expected at least {minimumCount} roles but found {assertingThat.InstanceToAssert.Roles.Count}");
        return assertingThat;
    }
}
