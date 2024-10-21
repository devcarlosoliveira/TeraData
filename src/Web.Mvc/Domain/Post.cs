using System.ComponentModel.DataAnnotations;
namespace Web.Mvc.Domain;

public class Post
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [Required]
    public Guid UserId { get; set; } = new Guid();
    public virtual User? User { get; set; }
    [Required]
    public Guid CategoryId { get; set; } = new Guid();
    public virtual Category? Category { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = [];
    public virtual ICollection<PostTag> PostTags { get; set; } = [];

    public Post() { }
}
