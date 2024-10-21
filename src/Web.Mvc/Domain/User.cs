using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
namespace Web.Mvc.Domain;

public class User : IdentityUser<Guid>
{
    public virtual ICollection<Post> Posts { get; set; } = [];
    public virtual ICollection<Comment> Comments { get; set; } = [];

    public User() { }
}
