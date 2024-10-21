using System.ComponentModel.DataAnnotations;

namespace Web.Mvc.Domain;

public class PostTag
{
    [Required]
    public Guid PostId { get; set; }
    public virtual Post? Post { get; set; }

    [Required]
    public Guid TagId { get; set; }
    public virtual Tag? Tag { get; set; }

    public PostTag() { }
}