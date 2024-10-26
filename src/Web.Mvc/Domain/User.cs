using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
namespace Web.Mvc.Domain;

public class User : IdentityUser<Guid>
{
    public virtual ICollection<Post> Posts { get; set; } = [];
    public virtual ICollection<Comment> Comments { get; set; } = [];

    public User() { }
}
/*
 * public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public string FirstAccessDate { get; set; } = string.Empty;

    public string LastAccessDate { get; set; } = string.Empty;

    public string AccessAttemptDate { get; set; } = string.Empty;

    public string AccessAttempt { get; set; } = string.Empty;

    public string MaxAccessAttempts { get; set; } = string.Empty;

    public string AccessNumber { get; set; } = string.Empty;

    public string IsAvailable { get; set; } = string.Empty;
 */