using System.ComponentModel.DataAnnotations;
namespace Web.Mvc.Domain;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Content { get; set; } = string.Empty;
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public Guid PostId { get; set; }
    public virtual Post? Post { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }

    public Comment() { }
}
