namespace JoanComasFdz.AssertingThat.Demo;

/// <summary>
/// Sample domain model representing a user account.
/// Used to demonstrate authentication and authorization assertion extensions.
/// </summary>
public class User
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool EmailVerified { get; set; }
    public List<string> Roles { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}
